using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace KESJCrawler.App_Code
{
    public class ReadConfig
    {
        public static List<itemsconfig> getConfigList()
        {
            String url1 = "http://127.0.0.1/test1.json";
            string json1;
            List<itemsconfig> items;


            HttpWebRequest request1 = (HttpWebRequest)HttpWebRequest.Create(url1);
            request1.UserAgent = "KESJ Web Crawler";
            WebResponse response1 = request1.GetResponse();
            Stream stream1 = response1.GetResponseStream();
            StreamReader r = new StreamReader(stream1);
            json1 = r.ReadToEnd();
            items = JsonConvert.DeserializeObject<List<itemsconfig>>(json1);





            /// TODO : Hier moet ik nog opvangen als JSON Configfile niet beschikbaar is
            //if (request1.Accept == null)
            //   System.Console.Write("Config file niet bereikbaar");  


            return items;




        }
    }
}