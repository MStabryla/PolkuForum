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
    public class SatireRepo : IRepoSat
    {
        IQueryable<Satire> IRepoSat.GetElements()
        {
            return Memory.db.Satires.Include(x => x.Author);
        }
        IQueryable<Satire> IRepoSat.GetElementsNoTracking()
        {
            return Memory.db.Satires.AsNoTracking().Include(x => x.Author);
        }
        Satire IRepoSat.GetElement(int? id)
        {
            return Memory.db.Satires.Include(x => x.Author).Include(x => x.Comments).ToList()[(int)id];
        }
        bool IRepoSat.AddSatire(Satire sat)
        {
            try
            {
                Memory.db.Satires.Add(sat);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoSat.RemoveSatire(Satire sat)
        {
            try
            {
                Memory.db.Satires.Remove(sat);
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoSat.EditSatire(Satire sat)
        {
            try
            {
                Memory.db.Entry(sat).State = EntityState.Modified;
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoSat.AddComment(ComS com)
        {
            try
            {
                Memory.db.CommentsOfSatire.Add(com);
                Memory.db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool IRepoSat.SetLike(LikeS like)
        {
            try
            {
                Memory.db.LikesOfSatire.Add(like);
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