using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolkuForum.Repo;
using PolkuForum.Models;
using PolkuForum.Models.Analyse;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace PolkuForum.Controllers
{
    public class MainController : Controller
    {
        private IRepoDis repod;
        private IRepoArt repoa;
        private IRepoSat repos;
        private ForumUser user;
        public MainController(IRepoDis repod, IRepoArt repoa,IRepoSat repos,IUserRepo users)
        {
            this.repoa = repoa;
            this.repod = repod;
            this.repos = repos;
            var test_user = User;
            if(User != null && User.Identity.IsAuthenticated)
            {
                user = users.GetElement(User.Identity.Name);
            }
        }
        [Route]
        public ActionResult Index()
        {
            ViewBag.Title = "Główna";
            return View();
        }
        public ActionResult MostPopular()
        {
            List<dynamic> list = new List<dynamic>();
            IEnumerable<Article> art = repoa.GetElementsNoTracking().OrderBy(x => x.Date).Take(3);
            IEnumerable<Discussion> dis = repod.GetElementsNoTracking().OrderBy(x => x.Date).Take(3);
            IEnumerable<Satire> sat = repos.GetElementsNoTracking().OrderBy(x => x.Date).Take(3);
            int[] integers = new int[] { art.Count(), dis.Count(), sat.Count() };
            int integer = (integers[0] > 3 && integers[1] > 3 && integers[2] > 3) ? 3 : integers.Min();
            for(var i=0;i<integer;i++)
            {
                var tymart = art.ElementAt(i);
                var tymdis = dis.ElementAt(i);
                var tymsat = sat.ElementAt(i);
                if(tymart.Date.Ticks > tymdis.Date.Ticks)
                {
                    if(tymart.Date.Ticks > tymsat.Date.Ticks)
                    {
                        list.Add(tymart);
                    }
                    else
                    {
                        list.Add(tymsat);
                    }
                }
                else
                {
                    if (tymdis.Date.Ticks > tymsat.Date.Ticks)
                    {
                        list.Add(tymdis);
                    }
                    else
                    {
                        list.Add(tymsat);
                    }
                }
            }
            return PartialView(list);
        }
        public ActionResult Articles()
        {
            List<Article> lista = repoa.GetElementsNoTracking().OrderBy(x => x.Date).Take(4).ToList();
            return PartialView(lista);
        }
        public ActionResult Satires()
        {
            List<Satire> lista = repos.GetElementsNoTracking().OrderBy(x => x.Date).Take(4).ToList();
            return PartialView(lista);
        }
        public ActionResult Disscussions()
        {
            List<Discussion> lista = repod.GetElementsNoTracking().OrderBy(x => x.Date).Take(4).ToList();
            return PartialView(lista);
        }
        public ActionResult Popular()
        {
            List<dynamic> list = new List<dynamic>();
            IEnumerable<Article> art = repoa.GetElementsNoTracking().OrderBy(x => x.Comments.Count).OrderBy(x => x.Date).Take(3);
            IEnumerable<Discussion> dis = repod.GetElementsNoTracking().OrderBy(x => x.Comments.Count).OrderBy(x => x.Date).Take(3);
            IEnumerable<Satire> sat = repos.GetElementsNoTracking().OrderBy(x => x.Comments.Count).OrderBy(x => x.Date).Take(3);
            list.AddRange(art);
            list.AddRange(dis);
            list.AddRange(sat);
            return PartialView(list);
        }
        [Authorize]
        public ActionResult Recommended()
        {
            List<dynamic> list = new List<dynamic>();
            try
            {
                Article art = repoa.GetElementsNoTracking().Where(x => user.Profile.Categories.Where(y => y == x.Category).Count() > 0).OrderBy(x => x.Comments.Count).OrderBy(x => x.Date).First();
                list.Add(art);
            }
            catch { }
            try
            {
                Discussion dis = repod.GetElementsNoTracking().Where(x => user.Profile.Categories.Where(y => y == x.Category).Count() > 0).OrderBy(x => x.Comments.Count).OrderBy(x => x.Date).First();
                list.Add(dis);
            }
            catch { }
            try
            {
                Satire sat = repos.GetElementsNoTracking().OrderBy(x => x.Comments.Count).OrderBy(x => x.Date).First();
                list.Add(sat);
            }
            catch { }
            return PartialView(list);
        }
        public ActionResult Today()
        {
            return PartialView(Memory.today);
        }
        [HttpGet]
        public ActionResult SetToday()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetToday(Days day)
        {
            try
            {
                var table = Request.Files;
                var file = Request.Files[0];
                var path = Server.MapPath("~/grf/") + day.Image;
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                file.SaveAs(path);
                day.Image = "/grf/" + day.Image;
                Memory.today = day;
                dynamic response = await MyClient.ConnectWithAPI(day.Description);
                if((bool)response.Passed)
                {
                    ViewBag.Confirmed = "false";
                    return RedirectToAction("Index");
                }
                ViewBag.Confirmed = "true";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Confirmed = "false";
                ViewData["error"] = "Błąd formularza";
                return RedirectToAction("Index");
            }
        }
    }
}