using System;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using PolkuForum.Repo;
using PolkuForum.Models;
using PolkuForum.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;

namespace PolkuForum.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<MainContext>();
            container.RegisterType<PolkuForumSignInManager>();
            container.RegisterType<PolkuForumUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<IRepoDis, DiscussionRepo>(new PerRequestLifetimeManager());
            container.RegisterType<IUserRepo, UserRepo>(new PerRequestLifetimeManager());
            container.RegisterType<IRepoArt, ArticleRepo>(new PerRequestLifetimeManager());
            container.RegisterType<IRepoSat, SatireRepo>(new PerRequestLifetimeManager());
            container.RegisterType<IRepoSou, SourceRepo>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType(typeof(IUserStore<ForumUser>), typeof(UserStore<ForumUser>));
            container.RegisterType<IUserStore<ForumUser>, UserStore<ForumUser>>(new InjectionConstructor(typeof(MainContext)));
        }
    }
}
