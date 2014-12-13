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

            string url = Config.Config.configUrl;
            String json;
            List<itemsconfig> items = null;
            //Problem reading Jsonfile from python. Make my own readable Json file
            String jsonFile = "[";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "KESJ Web Crawler";
            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (request.HaveResponse && response != null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            // Read out the Json file from python
                            json = reader.ReadToEnd();
                            dynamic deserializedValue = JsonConvert.DeserializeObject(json);
                            var values = deserializedValue["data"];

                            for (int i = 0; i < values.Count; i++)
                            {
                                jsonFile += values[i];
                                jsonFile += ",";
                            }
                            jsonFile += "]";

                            //Add to list for further reading
                            items = JsonConvert.DeserializeObject<List<itemsconfig>>(jsonFile);
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
                            //Log error and program will stop
                            LogFile.LogError(error, url);
                            
                        }
                    }
                }
            }

            return items;

        }
    }
}