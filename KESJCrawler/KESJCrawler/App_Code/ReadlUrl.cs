using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace KESJCrawler.App_Code
{
    public class ReadlUrl
    {
        public static string GetResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UserAgent = "KESJ Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string htmlText = reader.ReadToEnd();

            return htmlText;
        }
    }
}