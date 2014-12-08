using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace KESJCrawler.App_Code
{
    public class PostJson
    {

        public bool PostJsonWebCrawlToApi()
        {
            //try post json to python TODO: Make own class
            WebCrawl bastest = new WebCrawl();
            List<itemspost> test = bastest.CrawlWebsite();

            string bas = JsonConvert.SerializeObject(new { data = test }, Formatting.Indented);

            string url = "http://127.0.0.1:5000/";

            using (var client = new WebClient())
            {

                string userName = "user";
                string password = "1234";

                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + password));

                client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);
                client.Headers.Add("Content-Type", "application/json");
                var result = client.UploadString(url, "POST", bas);
            }
            


            return true;
        }

        

    }
}