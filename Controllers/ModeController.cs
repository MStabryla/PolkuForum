using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolkuForum.Controllers
{
    [Authorize(Roles="Editor")]
    [RoutePrefix("mode")]
    [Route("{action=HelpDesk}")]
    public class ModeController : Controller
    {
        // GET: Editors
        public ActionResult HelpDesk()
        {
            return View();
        }
        public ActionResult ReportedDiscussions()
        {
            return View();
        }
        public ActionResult Articles()
        {
            return View();
        }
        public ActionResult Sources()
        {
            return View();
        }
        public ActionResult Suggestions()
        {
            return View();
        }
    }
}