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
    public class DiscussionRepo : IRepoDis
    {
        
        IQueryable<Discussion> IRepoDis.GetElements()
        {
            return Memory.db.Discussions.Include(x => x.Category);
        }
        IQueryable<Discussion> IRepoDis.GetElementsNoTracking()
        {
            return Memory.db.Discussions.AsNoTracking().Include(x => x.Category);
        }
        Discussion IRepoDis.GetElement(int? id)
        {
            if (Memory.db.Discussions.ToList().Exists(x => x.Id == id))
            {
                return Memory.db.Discussions.Include(x => x.Category).Include(x => x.Comments).ToList()[(int)id];
            }
            return null;
        }
        IQueryable<Category> IRepoDis.GetCategories()
        {
            return Memory.db.Categories;
        }
        bool IRepoDis.CreateElement(Discussion obj)
        {
            try
            {
                Memory.db.Discussions.Add(obj);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoDis.RemoveElement(Discussion obj)
        {
            try
            {
                Memory.db.Discussions.Remove(obj);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoDis.EditElement(Discussion obj)
        {
            try
            {
                Memory.db.Entry(obj).State = EntityState.Modified;
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoDis.CreateComment(ComD obj)
        {
            try
            {
                //var tester = Memory.db.CommentsOfDiscussion.First(x => x.Author.Id == temuser.Id);
                //Memory.db.CommentsOfDiscussion.Add(obj);
                Memory.db.CommentsOfDiscussion.Add(obj);
                
                Memory.db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        bool IRepoDis.SetLike(LikeD obj)
        {
            try
            {
                Memory.db.LikesOfDiscussion.Add(obj);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoDis.Report(Report rep)
        {
            try
            {
                Memory.db.Reports.Add(rep);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}