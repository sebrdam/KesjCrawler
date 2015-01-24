using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/*
  Read url voor data crawl
  In opdracht van Hogeschool Rotterdam module : INFPRJ2110
  S. van Staden - 22-01-2015 - studentnr:0883388
*/

namespace KESJCrawler.App_Code
{
    public class ReadlUrl
    {
        public static string GetResponse(string url)
        {
            string result = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)";
            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (request.HaveResponse && response != null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                           
                        }
                    }
                }

                
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            //set string error max length 50
                            error = error.Substring(0, 50);
                            //Log the error and return the error for continue crawling next url
                            LogFile.LogMe(error, url);
                            return error;
                        }
                    }
                }
            }

            return result;

        }
    }
}