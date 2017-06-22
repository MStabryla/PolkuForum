using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PolkuForum.Models;

namespace PolkuForum.Repo
{
    
    public interface IRepoDis
    {
        IQueryable<Discussion> GetElements();
        IQueryable<Discussion> GetElementsNoTracking();
        Discussion GetElement(int? id);
        IQueryable<Category> GetCategories();
        bool CreateElement(Discussion obj);
        bool CreateComment(ComD obj);
        bool RemoveElement(Discussion obj);
        bool EditElement(Discussion obj);
        bool SetLike(LikeD obj);
        bool Report(Report rep);
    }
    public interface IUserRepo
    {
        IQueryable<ForumUser> GetElements();
        IQueryable<ForumUser> GetElementsNoTracking();
        ForumUser GetElement(int? id);
        ForumUser GetElement(string name);
        bool AddUser(ForumUser user,Profile profil);
        bool DeleteUser(int? id);
        bool EditProfile(Profile newpro);
    }
    public interface IRepoArt
    {
        IQueryable<Article> GetElements();
        IQueryable<Article> GetElementsNoTracking();
        Article GetElement(int? id);
        Source GetSource(int? id);
        bool AddArticle(Article art);
        bool RemoveArticle(Article art);
        bool AddComment(ComA com);
        bool SetLike(LikeA like);
    }
    public interface IRepoSou
    {
        IQueryable<Source> GetElements();
        IQueryable<Source> GetElementsNoTracking();
        Source GetElement(int? id);
        bool AddSource(Source sou);
        bool RemoveSource(Source sou);
        bool EditSource(Source sou);
    }
    public interface IRepoSat
    {
        IQueryable<Satire> GetElements();
        IQueryable<Satire> GetElementsNoTracking();
        Satire GetElement(int? id);
        bool AddSatire(Satire sat);
        bool RemoveSatire(Satire sat);
        bool EditSatire(Satire sat);
        bool AddComment(ComS com);
        bool SetLike(LikeS like);
    }
}