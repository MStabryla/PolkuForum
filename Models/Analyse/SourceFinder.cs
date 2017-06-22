using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using PolkuForum.Repo;
using System.Diagnostics;

namespace PolkuForum.Models.Analyse
{
    public static class Memory
    {
        public static Days today = new Days { Title = "tytuł", Description = "Opis dnia", Image = "/grf/wydarzenie.jpg" };
        public static MainContext db
        { get; set; }
    }

    public static class SourceFinder
    {
        public static bool SourceExist(string addr,IRepoSou repo)
        {
            string domain = GetDomain(addr);
            return repo.GetElements().Any(x => x.Domain == domain);
        }
        public static string GetDomain(string addr)
        {
            addr = addr.Trim();
            addr = addr.Replace("http://", "");
            addr = addr.Replace("https://", "");
            int domainend = addr.IndexOf('/');
            string domain = "";
            for (int i = 0; i < domainend; i++)
            {
                domain += addr[i];
            }
            return domain;
        }
        public static bool SourceWorks(string addr)
        {
            try
            {
                MyClient client = new MyClient(new System.Net.Http.HttpClient());
                string response = client.GetString(addr);
                //WebRequest req = HttpWebRequest.Create(addr);
                //req.Method = "GET";
                if (!response.Contains("<html"))
                {
                    return false;
                }
                return true;
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}