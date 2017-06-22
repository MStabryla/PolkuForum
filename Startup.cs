using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;
using PolkuForum.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataProtection;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(PolkuForum.Startup))]

namespace PolkuForum
{
    public class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; set; }
        public void Configuration(IAppBuilder app)
        {
            Startup.DataProtectionProvider = app.GetDataProtectionProvider();
            //app.CreatePerOwinContext(MainContext.Create);
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/konto"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager<Models.ForumUser>, Models.ForumUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenUserIdentity(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            /*app.CreatePerOwinContext<UserManager<ForumUser>>(delegate ()
            {
                var manager = new UserManager<ForumUser>(new UserStore<ForumUser>(new MainContext()));
                
                return manager;
            });*/
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<PolkuForumUserManager>());
           /*app.CreatePerOwinContext<SignInManager<ForumUser, string>>((IdentityFactoryOptions<SignInManager<ForumUser, string>> manager, IOwinContext context) => 
            {
                var Man = context.GetUserManager<UserManager<ForumUser>>();
                return new SignInManager<Models.ForumUser, string>(context.GetUserManager<UserManager<Models.ForumUser>>(), context.Authentication);
            });*/
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
    }
}
