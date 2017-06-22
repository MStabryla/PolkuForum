using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace PolkuForum.Models
{
    class MyClient
    {
        private HttpClient client
        { get; set; }
        public MyClient(HttpClient client)
        {
            this.client = client;
        }
        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }
        public string GetString(string url)
        {
            return GetStringAsync(url).Result;
        }
        public async Task<string> GetStringAsync(string url)
        {
            return await client.GetStringAsync(url);
        }
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            
            return await client.GetAsync(url);
        }
        public HttpResponseMessage Post(string url,HttpContent content)
        {
            return PostAsync(url, content).Result;
        }
        public async Task<HttpResponseMessage> PostAsync(string url,HttpContent content)
        {
            return await client.PostAsync(url,content);
        }
        public static async Task<dynamic> ConnectWithAPI(string comment)
        {
            MyClient client = new MyClient(new HttpClient());
            var form = new List<KeyValuePair<string, string>>();
            form.Add(new KeyValuePair<string, string>("ClientName", "PoluForum"));
            form.Add(new KeyValuePair<string, string>("Comment", comment));
            var resp = await client.PostAsync("http://polkuforumapi.azurewebsites.net/api/werify?dotnetaccepted=true", new FormUrlEncodedContent(form));
            var jsonresp = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(jsonresp);
        }
    }
}
