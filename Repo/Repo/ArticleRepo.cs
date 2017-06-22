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
    public class ArticleRepo : IRepoArt
    {
        IQueryable<Article> IRepoArt.GetElements()
        {
            return Memory.db.Articles.Include(x => x.Source).Include(x => x.Category);
        }
        IQueryable<Article> IRepoArt.GetElementsNoTracking()
        {
            return Memory.db.Articles.AsNoTracking().Include(x => x.Source).Include(x => x.Category);
        }
        Article IRepoArt.GetElement(int? id)
        {
            return Memory.db.Articles.Include(x => x.Comments).Include(x => x.Category).Include(x => x.Source).ToList()[(int)id];
        }
        Source IRepoArt.GetSource(int? id)
        {
            return Memory.db.Sources.Include(x => x.Credibility).ToList()[(int)id];
        }
        bool IRepoArt.AddArticle(Article art)
        {
            try
            {
                Memory.db.Articles.Add(art);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoArt.RemoveArticle(Article art)
        {
            try
            {
                Memory.db.Articles.Remove(art);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoArt.AddComment(ComA com)
        {
            try
            {
                Memory.db.CommentsOfArticle.Add(com);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoArt.SetLike(LikeA like)
        {
            try
            {
                Memory.db.LikesOfArticle.Add(like);
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