using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolkuForum.Controllers
{
    [RoutePrefix("contact")]
    [Route("{action=Help}")]
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Help()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult WriteToEditors()
        {
            return View();
        }
        [Authorize]
        public ActionResult MySuggestions()
        {
            return View();
        }
        public ActionResult AboutAPI()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}