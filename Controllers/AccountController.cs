using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolkuForum.Repo;
using PolkuForum.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
namespace PolkuForum.Controllers
{
    [Route("{action=Login}")]
    [RoutePrefix("konto")]
    public class AccountController : Controller
    {
        private ForumUser user;
        private IUserRepo repo;
        private PolkuForumUserManager UserManagerRepo;
        private PolkuForumSignInManager SignManagerRepo;
        private IAuthenticationManager authenticationManager;
        /*public AccountController()
        {
            var test = new PolkuForumUserManager(new UserStore<ForumUser>(new MainContext()));
            if (User != null && User.Identity.IsAuthenticated)
            {
                user = repo.GetElement(User.Identity.Name);
            }
        }*/
        public AccountController(PolkuForumUserManager usermanager, PolkuForumSignInManager signmanager,IAuthenticationManager authmanager,IUserRepo repo)
        {
            this.repo = repo;
            var test = new PolkuForumUserManager(new UserStore<ForumUser>(new MainContext()));
            UserManagerRepo = usermanager;
            SignManagerRepo = signmanager;
            authenticationManager = authmanager;
            if (User != null && User.Identity.IsAuthenticated)
            {
                user = repo.GetElement(User.Identity.Name);
            }
        }
        // GET: Account
        [HttpGet]
        [Route("zaloguj")]
        public ActionResult Login()
        {
            ViewBag.Title = "Zaloguj";
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("GetActualUser");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("zaloguj")]
        public ActionResult Login(LoginView model)
        {
            ViewBag.Title = "Zaloguj";
            if (ModelState.IsValid)
            {
                if(SignManagerRepo.PasswordSignIn<ForumUser,string>(model.Login,model.Password,model.Remember,false) == SignInStatus.Success)
                {
                    return RedirectToAction("GetActualUser");
                }
                else
                {
                    ViewBag.Error = "Błędny login użytkownika lub hasło";
                    return View("Login",model);
                }
            }
            ViewBag.Error = "Błąd formularza - wypełnij ponownie";
            return View("Login",model);
        }
        public ActionResult PartialLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("UserMini",repo.GetElement(User.Identity.Name));
            }
            return PartialView();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PostPartialLogin([Bind(Include = "Login,Password")]LoginView model)
        {
            if (ModelState.IsValid)
            {
                if (SignManagerRepo.PasswordSignIn<ForumUser, string>(model.Login, model.Password, model.Remember, false) == SignInStatus.Success)
                {
                    return RedirectToAction("GetActualUser");
                }
                else
                {
                    ViewData["error"] = "Błędny login użytkownika lub hasło";
                    return RedirectToAction("Login",model);
                }
            }
            ViewData["error"] = "Błąd formularza - wypełnij ponownie";
            return RedirectToAction("Login",model);
        }
        [HttpGet]
        [Route("rejestracja")]
        public ActionResult NewAccount()
        {
            ViewBag.Title = "Dodaj użytkownika";
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("GetActualUser");
            }
            return View();
        }
        [HttpPost]
        [Route("rejestracja")]
        [ValidateAntiForgeryToken]
        public ActionResult NewAccount(CreateUser newuser)
        {
            ViewBag.Title = "Dodaj użytkownika";
            if (ModelState.IsValid)
            {
                var forumuser = new ForumUser { Nick = newuser.Nick, UserName = newuser.Login ,Email = newuser.Email,PasswordHash = newuser.Password , Role = "User"};
                var profile = new Profile { Decription = newuser.Description, Obraz = newuser.Image, Education = newuser.Education };
                forumuser.Profile = profile;
                var result = UserManagerRepo.Create(forumuser, newuser.Password);
                if (result.Succeeded)
                {
                    return Json(new { completed=true,link=Url.Action("GetActualUser")});
                }
                else
                {
                    var res = "";
                    foreach(string r in result.Errors)
                    {
                        res += r + " ";
                    }
                    return Json(new { completed = false,error=res});
                }
            }
            return Json(new { completed=false,error = "Błąd formularza - wypełnij ponownie" });
        }
        [HttpPost]
        public ActionResult SendImage(SendImage model)
        {
            try
            {
                var file = Request.Files[0];
                var path = Server.MapPath("~/grf/" + model.filename);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                file.SaveAs(path);
                return Json(new { success=true,path="/grf/" + model.filename});
            }
            catch
            {
                ViewData["Error"] = "Błąd zapisu pliku";
                return Json(new { success = false, path = "" });
            }
        }
        [Authorize]
        [Route("profil")]
        public ActionResult GetActualUser()
        {
            ViewBag.Title = User.Identity.Name;
            return View("GetUser",repo.GetElement(User.Identity.Name));
        }
        [Route("{name}")]
        public ActionResult GetUser(string name)
        {
            ViewBag.Title = repo.GetElement(name).Nick;
            return View(repo.GetElement(name));
        }
        [Route("wyloguj")]
        [Authorize]
        public ActionResult LogOff()
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Main");
        }
    }
}