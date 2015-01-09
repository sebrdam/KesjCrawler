using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace KESJCrawler.App_Code
{
    public class PostJson
    {

        public string PostJsonWebCrawlToApi()
        {
            
            WebCrawl crawl = new WebCrawl();
            List<itemspost> data = crawl.CrawlWebsite();

            
            var result = "";

            string post = JsonConvert.SerializeObject(new { data = data }, Formatting.Indented);

            //Write post to sql file. For backup if post don't succeed
            WritePostToSql.WriteToFile(post);

            string url = Config.Config.postUrl;

            using (var client = new WebClient())
            {

                string userName = Config.Config.userName;
                string password = Config.Config.password;

                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + password));

                client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);
                client.Headers.Add("Content-Type", "application/json");
                result = client.UploadString(url, "POST", post);
                
            }

            
            return result;
        }

        

    }
}