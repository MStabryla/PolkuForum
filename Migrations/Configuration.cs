namespace PolkuForum.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Security;
    using Newtonsoft.Json;

    internal sealed class Configuration : DbMigrationsConfiguration<MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MainContext context)
        {
            SeedRoles(context);
            SeedAdmin(context);
            //SeedSimpleBase(context);
        }
        private void SeedRoles(MainContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            string[] roles = new string[] { "Admin", "Artist", "Mode", "User" };
            foreach(string roleName in roles)
            {
                if(!roleManager.RoleExists(roleName))
                {
                    var role = new IdentityRole();
                    role.Name = roleName;
                    roleManager.Create(role);
                }
            }
        }
        private void SeedAdmin(MainContext context)
        {
            var userManager = new PolkuForumUserManager(new UserStore<ForumUser>(context));
            if(!context.Users.Any(x => x.UserName == "Admin"))
            {
                string line = " 48";
                var hasher = new PasswordHasher();
                var admin = new ForumUser { UserName = "Admin" , Nick = "admin",Role = "Admin",Email  = "login@polkuforum.pl"};
                var profile = new Profile { Decription = "Test", Education="",Obraz = "grf/user-icon.png", User = admin };
                admin.Profile = profile;
                var result = userManager.Create(admin, "Admin_23");
                if (result.Succeeded)
                {
                    try
                    {
                        //admin = userManager.Find("Admin", "admin23");
                        profile.User = admin;
                        context.Profiles.Add(profile);
                        context.Entry(profile).State = EntityState.Added;
                        context.Entry(admin).State = EntityState.Modified;
                        context.SaveChanges();
                        var addtoroleresult = userManager.AddToRole(admin.Id, "Admin");
                        if (addtoroleresult.Succeeded)
                        {
                            throw new Exception("Uda³o siê " + JsonConvert.SerializeObject(addtoroleresult));
                        }
                        else
                        {
                            throw new Exception(JsonConvert.SerializeObject(addtoroleresult) + line);
                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        System.IO.StringWriter sw = new System.IO.StringWriter();
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            sw.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                sw.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw new Exception(sw.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(JsonConvert.SerializeObject(ex) + "\n" + admin.GetHashCode() + "   " + profile.User.GetHashCode() + "\n" + line);
                    }
                }
                else
                {
                    throw new Exception(JsonConvert.SerializeObject(result) + line);
                }
            }
            else
            {
                throw new Exception("Usuñ admina");
            }
        }
        private void SeedSimpleBase(MainContext context)
        {
            var userManager = new PolkuForumUserManager(new UserStore<ForumUser>(context));
            var user = new ForumUser { UserName = "JKowalski", Nick = "Jan Kowalski", Role = "User", Email = "jkowalkski@poczta.pl" };
            if (!context.Users.Any(x => x.UserName == "JKowalski"))
            {
                var profile = new Profile { Decription = "Jan Kowalski", Education = "Nauki Polityczne - Uniwersytet Jagieloñski w Krakowie", Obraz = "grf/user-icon.png", User = user };
                user.Profile = profile;
                profile.User = user;
                var result = userManager.Create(user, "Test1234");
                if(result.Succeeded)
                {
                    try
                    {
                        //admin = userManager.Find("Admin", "admin23");
                        context.Profiles.Add(profile);
                        /*context.Entry(profile).State = EntityState.Added;
                        context.Entry(admin).State = EntityState.Modified;*/
                        context.SaveChanges();
                        var addtoroleresult = userManager.AddToRole(user.Id, "User");
                        if (!addtoroleresult.Succeeded)
                        {
                            throw new Exception("Nie uda³o siê utworzyæ u¿ytkownika " + JsonConvert.SerializeObject(addtoroleresult));
                        }
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("B³¹d wprowadzania danych " + JsonConvert.SerializeObject(ex));
                    }
                }
            }
            if(context.Discussions.Count() < 3)
            {
                if(context.Categories.Count() < 2)
                {
                    var category1 = new Category() { Name = "Gospodarka", Description = "Wszystko zwi¹zane z gospodark¹", Color = "74fd54",Icon = "grf/gospo.png" };
                    context.Categories.Add(category1);
                    var category2 = new Category() { Name = "Kultura", Description = "Wszystko zwi¹zane z kultur¹", Color = "7B2BD0", Icon = "grf/kultu.png" };
                    context.Categories.Add(category2);
                }
                if(context.Sources.Count() < 2)
                {
                    var source1 = new Source() { Domain="wyborcza.pl",Image="grf/wyb.png",Description="DOmena nale¿¹ca do gazety i dziennika internetowego Gazeta Wyborcza",Name="Gazeta Wyborcza"};
                    var source2 = new Source() { Domain = "wolnosc24.pl", Image = "grf/wol.png", Description = "Domena nale¿¹ca do gazety Najwy¿szy czas i portalu Wolnoœæ24.pl", Name = "Wolnoœæ 24" };
                    context.Sources.Add(source1);
                    context.Sources.Add(source2);
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("B³¹d tworzenia danych podstawowych " + JsonConvert.SerializeObject(ex));
                }
                var mainsource = context.Sources.First();
                var maincategory = context.Categories.First();
                for(var i=1;i<=3;i++)
                {
                    var discussion = new Discussion() { Author = user, Title = "DyskusjaTest" + i, Decription = "Przyk³adowy opis", Date = DateTime.Now,Category = maincategory };
                    for(var j=0;j<3;j++)
                    {
                        var comment = new ComD() {Author = user,Kind = true, Date=DateTime.Now,Content="Treœæ"+i+j,Discussion = discussion};
                        context.CommentsOfDiscussion.Add(comment);
                    }
                    context.Discussions.Add(discussion);
                }
                for (var i = 1; i <= 3; i++)
                {
                    var article = new Article() { Author=user,Title="Artyku³Test"+i,Category = maincategory,Address= "http://wolnosc24.pl/2017/02/25/glowny-inspektor-pracy-zapowiada-beda-kontrole-w-wymiarze-totalnym-panstwo-nieprzyjazne-przedsiebiorcom/",Date=DateTime.Now,Description="Opis artyku³u",Source=mainsource};
                    for (var j = 0; j < 3; j++)
                    {
                        var comment = new ComA() { Author = user, Date = DateTime.Now, Content = "Treœæ" + i + j, Article = article,Mark = 3 };
                        context.CommentsOfArticle.Add(comment);
                    }
                    context.Articles.Add(article);
                }
                for(var i=1;i<=3;i++)
                {
                    var satire = new Satire() { Author = user, Title = "SatireTitle" + i, Description = "OpisSatyry"+i, Date = DateTime.Now, Tags = "test,tag" };
                    satire.Kind = i == 1 ? "Picture" : (i == 2 ? "Poem" : "Movie");
                    for(var j=0;j<3;j++)
                    {
                        var comment = new ComS() { Author = user, Date=DateTime.Now,Content="Treœæ"+i+j,Satire = satire};
                        context.CommentsOfSatire.Add(comment);
                    }
                    context.Satires.Add(satire);
                }
                try
                {
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw new Exception("B³¹d tworzenia danych" + JsonConvert.SerializeObject(ex));
                }
                
            }
        }
    }
    
}
