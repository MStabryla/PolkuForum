using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PolkuForum.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PolkuForum.Models.Analyse;

namespace PolkuForum.Repo
{
    public class UserRepo :  IUserRepo
    {
        IQueryable<ForumUser> IUserRepo.GetElements()
        {
            return Memory.db.Users.Include(x => x.Profile);
        }
        IQueryable<ForumUser> IUserRepo.GetElementsNoTracking()
        {
            return Memory.db.Users.AsNoTracking().Include(x => x.Profile);
        }
        ForumUser IUserRepo.GetElement(int? id)
        {
            return Memory.db.Users.ToList()[(int)id];
        }
        ForumUser IUserRepo.GetElement(string name)
        {
            return Memory.db.Users.Include(x => x.Profile).First(x => x.UserName == name);
        }
        bool IUserRepo.AddUser(ForumUser user,Profile profil)
        {
            if (Memory.db.Users.Count(x => x.UserName == user.UserName) > 0)
            {
                return false;
            }
            else
            {
                var manager = new UserManager<ForumUser>(new UserStore<ForumUser>());
                var profile = profil;
                var result = manager.Create(user, user.PasswordHash);
                Memory.db.Profiles.Add(profil);
                if (result.Succeeded)
                {
                    try
                    {
                        Memory.db.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
                    manager.AddToRole(user.Id, "User");
                    return true;
                }
                return false;
            }
        }
        bool IUserRepo.DeleteUser(int? id)
        {
            if (id == null)
            {
                return false;
            }
            try
            {
                var manager = new UserManager<ForumUser>(new UserStore<ForumUser>());
                var user = manager.FindByIdAsync(id.ToString());
                var logins = user.Result.Logins;
                var rolesForUser = manager.GetRolesAsync(id.ToString());

                using (var transaction = Memory.db.Database.BeginTransaction())
                {
                    foreach (var login in logins.ToList())
                    {
                        manager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }

                    if (rolesForUser.Result.Count() > 0)
                    {
                        foreach (var item in rolesForUser.Result.ToList())
                        {
                            // item should be the name of the role
                            var result = manager.RemoveFromRoleAsync(user.Id.ToString(), item);
                        }
                    }
                    manager.DeleteAsync(user.Result);
                    transaction.Commit();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IUserRepo.EditProfile(Profile newpro)
        {
            return true;
        }
    }
}