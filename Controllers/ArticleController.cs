using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolkuForum.Repo;
using PolkuForum.Models;

namespace PolkuForum.Controllers
{
    [RoutePrefix("artykuły")]
    [Route("{action=Latest}")]
    public class ArticleController : Controller
    {
        private ForumUser user;
        private readonly IRepoArt repo;
        private readonly IRepoSou repoSou;
        private readonly IUserRepo repouser;
        public ArticleController(IRepoArt repo, IUserRepo users, IRepoSou repoSou)
        {
            this.repoSou = repoSou;
            this.repo = repo;
            this.repouser = users;
            if (User != null && User.Identity.IsAuthenticated)
                user = users.GetElement(User.Identity.Name);
        }
        // GET: Article
        public ActionResult Latest()
        {
            List<Article> lista = repo.GetElementsNoTracking().OrderBy(x => x.Date).Take(10).ToList();
            return View("Articles", lista);
        }
        public ActionResult MostActive()
        {
            List<Article> lista = repo.GetElementsNoTracking().OrderBy(x => x.Comments.Count).Take(10).ToList();
            return View("Articles", lista);
        }
        [Authorize]
        public ActionResult MySources()
        {
            user = repouser.GetElement(User.Identity.Name);
            List<Article> lista = repo.GetElements().Where(x => user.Sources.Where(y => y == x.Source).Count() > 0).ToList();
            return View("Articles", lista);
        }
        [Route("{id:int}")]
        public ActionResult Article(int? id)
        {
            return View(repo.GetElement((int)id));
        }
        public ActionResult Sources()
        {
            List<Source> lista = repoSou.GetElements().OrderBy(x => x.Name).Take(10).ToList();
            return View(lista);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Mode")]
        public ActionResult AddSource()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Mode")]
        public ActionResult AddSource(CreateSource model)
        {
            Source sou = new Source { Name = model.Name, Description = model.Description, Domain = model.Domain, Image = model.Image };
            return Content(repoSou.AddSource(sou).ToString());
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View(repoSou.GetElements());
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateArticle model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Article art = new Article { Title = model.Name, Description = model.Description, Address = model.Address };
            if (model.NewSource && !Models.Analyse.SourceFinder.SourceExist(model.Address, repoSou) && Models.Analyse.SourceFinder.SourceWorks(model.Address))
            {
                Source sou = new Source { Domain = Models.Analyse.SourceFinder.GetDomain(model.Address), Name = Models.Analyse.SourceFinder.GetDomain(model.Address), Description = "New source", Image = "grf/defaultSou.png" };
                art.Source = sou;
                return Content((repoSou.AddSource(sou) && repo.AddArticle(art)).ToString());
            }
            else if (Models.Analyse.SourceFinder.SourceExist(model.Address, repoSou))
            {
                string domain = Models.Analyse.SourceFinder.GetDomain(model.Address);
                Source sou = repoSou.GetElements().First(x => x.Domain == domain);
                art.Source = sou;
                return Content(repo.AddArticle(art).ToString());
            }
            else
            {
                art.Source = repoSou.GetElements().First(x => x.Id == model.SourceId);
                return Content(repo.AddArticle(art).ToString());
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin,Mode")]
        public ActionResult EditSource(int? id)
        {
            return View(repo.GetElement((int)id));
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Mode")]
        public ActionResult EditSource(CreateSource model)
        {
            Source sou = repoSou.GetElements().ToList()[model.Id];
            return Content(repoSou.EditSource(sou).ToString());
        }
    }
}