using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolkuForum.Models
{
    public class PolkuForumUserManager : UserManager<ForumUser>
    {
        public PolkuForumUserManager(IUserStore<ForumUser> store) : base(store)
        {
            this.UserValidator = new UserValidator<ForumUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");
                this.UserTokenProvider =
                    new DataProtectorTokenProvider<ForumUser>(dataProtector);
            }
        }
    }
    public class PolkuForumSignInManager : SignInManager<ForumUser,string>
    {
        public PolkuForumSignInManager(PolkuForumUserManager umanager,IAuthenticationManager authmanager) : base(umanager,authmanager)
        {

        }
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ForumUser user)
        {
            return user.GenUserIdentity((PolkuForumUserManager)UserManager);
        }
    }
}