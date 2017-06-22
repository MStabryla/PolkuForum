using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolkuForum.Repo;
using PolkuForum.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PolkuForum.Controllers
{
    [RoutePrefix("dyskusje")]
    [Route("{action=Latest}")]
    public class DiscussionController : Controller
    {
        private ForumUser user;
        private readonly IRepoDis repo;
        private readonly IUserRepo userrepo;
        public DiscussionController(IRepoDis rep,IUserRepo users)
        {
            repo = rep;
            userrepo = users;
        }
        // GET: Discussion
        public ActionResult Latest()
        {
            ViewBag.Title = "Najnowsze dyskusje";
            List<Discussion> lista = repo.GetElementsNoTracking().OrderByDescending(x => x.Date).Take(10).ToList();
            return View("Discussions",lista);
        }
        public ActionResult MostActive()
        {
            ViewBag.Title = "Najpopularniejsze dyskusje";
            List<Discussion> lista = repo.GetElementsNoTracking().OrderByDescending(x => x.Comments.Count).Take(10).ToList();
            return View("Discussions",lista);
        }
        [Route("kategorie/{name}")]
        public ActionResult Category(string name)
        {
            Category cat = repo.GetCategories().Where(x => x.Name == name).First();
            ViewBag.Title = cat.Name + " dyskusja ";
            List<Discussion> lista = repo.GetElementsNoTracking().Where(x => x.Category == cat).OrderByDescending(x => x.Date).ToList();
            return View("Discussions", lista);
        }
        [Authorize]
        public ActionResult MyFauvorite()
        {
            ViewBag.Title = "Ulubione dyskusje";
            user = userrepo.GetElement(User.Identity.Name);
            List<Discussion> lista = repo.GetElementsNoTracking().Where(x => user.Profile.Categories.Where(y => y == x.Category).Count() > 0).OrderBy(x => x.Category.Name).Take(10).ToList();
            return View("Discussions", lista);
        }
        /// <summary>
        /// Otworzenie konkretnej dyskusji
        /// </summary>
        /// <param name="id">Identyfikator dyskusji</param>
        /// <returns>Dyskusja</returns>
        [Route("{id:int}")]
        public ActionResult Discussion(int id)
        {
            return View(repo.GetElementsNoTracking().Where(x => x.Id == id).First());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateDis model)
        {
            if(!ModelState.IsValid)
            {
                return Content("false");
            }
            var dis = new Discussion() { Title = model.Title, Decription = model.Description };
            dis.Author = userrepo.GetElement(User.Identity.Name);
            dis.Date = DateTime.Now;
            dis.Category = repo.GetCategories().First(x => x.Id == model.CategoryId);
            if(repo.CreateElement(dis))
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddComment(int? id)
        {
            ViewData["disID"] = id;
            return PartialView();
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddComment(CreateComment model)
        {
            
            if (!ModelState.IsValid)
            {
                return Content("false");
            }
            var com = new ComD { Content = model.Content };
            com.Date = DateTime.Now;
            com.Author = userrepo.GetElement(User.Identity.Name);
            com.Discussion = repo.GetElements().First(x => x.Id == model.PostID);
            com.Kind = model.Side;
            dynamic response = await MyClient.ConnectWithAPI(model.Content);
            if((bool)response.Passed)
            {
                if (repo.CreateComment(com))
                {
                    return Json(new { error = "", success = true, response =JsonConvert.SerializeObject(response) });
                }
                else
                {
                    return Json(new { error = "Base", success = false, response = JsonConvert.SerializeObject(response) });
                }
            }
            else
            {
                return Json(new { error="NotPassed",success=false,response= JsonConvert.SerializeObject(response) });
            }
        }
            
        [Authorize]
        [HttpPost]
        public ActionResult Like(CreateLike model)
        {
            if(repo.GetElements().ToList()[model.DisId].Comments.Where(x => x.Author == user).Count() > 0)
            {
                LikeD like = new LikeD();
                like.Side = repo.GetElements().ToList()[model.DisId].Comments.First(x => x.Author == user).Kind;
                like.Author = user;
                like.Comment = repo.GetElements().ToList()[model.DisId].Comments.ToList()[model.ComId];
                if(repo.SetLike(like))
                {
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
            return Content("false");
        }
        [Authorize]
        [HttpPost]
        public ActionResult Report(CreateReport model)
        {
            var rep = new Report() { Substantiation = model.Substantiation };
            rep.Author = user;
            rep.Discussion = repo.GetElements().ToList()[model.DisId];
            if(repo.Report(rep))
            {
                return Content("true"); 
            }
            else
            {
                return Content("false");
            }
        }
    }
}