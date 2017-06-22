using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolkuForum.Models;
using PolkuForum.Repo;

namespace PolkuForum.Controllers
{
    [RoutePrefix("satyry")]
    [Route("{action=Latest}")]
    public class SatireController : Controller
    {
        private ForumUser user;
        private readonly IRepoSat repo;
        private readonly IUserRepo userepo;
        public SatireController(IRepoSat repo,IUserRepo users)
        {
            this.repo = repo;
            if (User != null && User.Identity.IsAuthenticated)
                user = users.GetElement(User.Identity.Name);
        }
        // GET: Satire
        public ActionResult Latest()
        {
            List<Satire> lista = repo.GetElements().OrderBy(x => x.Date).ToList();
            return View("Satires",lista);
        }
        public ActionResult Pictures()
        {
            List<Satire> lista = repo.GetElements().Where(x => x.Kind == "Pic").OrderBy(x => x.Date).ToList();
            return View("Satires", lista);
        }
        public ActionResult Poems()
        {
            List<Satire> lista = repo.GetElements().Where(x => x.Kind == "Poe").OrderBy(x => x.Date).ToList();
            return View("Satires",lista);
        }
        public ActionResult Films()
        {
            List<Satire> lista = repo.GetElements().Where(x => x.Kind == "Fil").OrderBy(x => x.Date).ToList();
            return View("Satires",lista);
        }
        [Route("{id:int}")]
        public ActionResult Satire(int? id)
        {
            return PartialView(repo.GetElements().ToList()[(int)id]);
        }
        [Authorize]
        public ActionResult MyFauvorite()
        {
            user = userepo.GetElement(User.Identity.Name);
            List<Satire> lista = repo.GetElements().Where(x => x.Kind == "Fil").OrderBy(x => x.Date).ToList();
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Artist")]
        public ActionResult AddSatire()
        {
            
            return Content("true");
        }
    }
}