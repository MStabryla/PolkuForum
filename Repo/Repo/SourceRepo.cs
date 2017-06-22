using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PolkuForum.Models;
using System.Data.Entity;
using PolkuForum.Models.Analyse;

namespace PolkuForum.Repo
{
    public class SourceRepo : IRepoSou
    {
        IQueryable<Source> IRepoSou.GetElements()
        {
            return Memory.db.Sources.Include(x => x.Credibility);
        }
        IQueryable<Source> IRepoSou.GetElementsNoTracking()
        {
            return Memory.db.Sources.AsNoTracking().Include(x => x.Credibility);
        }
        Source IRepoSou.GetElement(int? id)
        {
            return Memory.db.Sources.Include(x => x.Credibility).ToList()[(int)id];
        }
        bool IRepoSou.AddSource(Source sou)
        {
            try
            {
                Memory.db.Sources.Add(sou);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoSou.RemoveSource(Source sou)
        {
            try
            {
                Memory.db.Sources.Remove(sou);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoSou.EditSource(Source sou)
        {
            try
            {
                Memory.db.Entry(sou).State = EntityState.Modified;
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