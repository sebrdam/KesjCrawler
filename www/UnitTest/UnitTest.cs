using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Net;
using KESJCrawler.Config;
using Newtonsoft.Json;
using System.IO;
using KESJCrawler;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using KESJCrawler.App_Code;
using System.Web.DynamicData;

/*
  Unit test test system en componenten
  In opdracht van Hogeschool Rotterdam module : INFPRJ2110
  S. van Staden - 22-01-2015 - studentnr:0883388
*/

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestPostApi()
        {
            var result = "";
            List<itemspost> data = new List<itemspost>();
            data.Add(new itemspost { omschrijving = "Unittest", category = "Unittest", subcategory = "Unittest", linkurl = "Unittest", prijs = 1000, provider = "Unittest", picurl = "Unittest", dataspecs = "Unittest" });
            

            string post = JsonConvert.SerializeObject(new { data = data }, Formatting.Indented);

            string url = Config.postUrl;

            using (var client = new WebClient())
            {

                string userName = Config.userName;
                string password = Config.password;

                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + password));

                client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);
                client.Headers.Add("Content-Type", "application/json");
                result = client.UploadString(url, "POST", post);

            }


            //return result;
            Console.WriteLine(result);
        }

        [TestMethod]
        public void TestResultsFromApi()
        {
            String url = "http://95.85.57.61/results/Moederbord";
            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = request.GetResponse();

            //return response;
            Console.WriteLine(response);
        }

        [TestMethod]
        public void TestLogFile()
        {
                        
            LogFile.LogMe("Unit Test", "Unit Test");
                        
        }

        [TestMethod]
        public void TestWritePostToFile()
        {
            
            List<itemspost> data = new List<itemspost>();
            data.Add(new itemspost { omschrijving = "Unittest", category = "Unittest", subcategory = "Unittest", linkurl = "Unittest", prijs = 1000, provider = "Unittest", picurl = "Unittest", dataspecs = "Unittest" });
            string post = JsonConvert.SerializeObject(new { data = data }, Formatting.Indented);

            //Write post to sql file. 
            WritePostToSql.WriteToFile(post);

        }

        [TestMethod]
        public void TestFrontend()
        {
            String url = "http://5.157.83.239/kesj/#/components/list/Processor";
            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = request.GetResponse();

            //return response;
            Console.WriteLine(response);
        }

        [TestMethod]
        public void TestMatchRegexFirstPatternAlternate()
        {
            String subpatternomschrijving = "<span class=\"pic\" title=\"(.*?)\"";
            String subpatternlink = "<a class=\"productLink\" href=\"(.*?)\"";
            String subpatternprijs = "&euro; (.*?)</sup>";
            String picurl = "image:url(.*?)\"></span>";
            String pattern = "<div class=\"listRow\">(.*?)<div class=\"clear\">";
            String url = "http://www.alternate.nl/html/product/listing.html?navId=20673&tk=7&lk=13777";
            Regex firstPattern = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string htmlText = ReadlUrl.GetResponse(url);

            MatchCollection first = firstPattern.Matches(htmlText);

            if (first.Count == 0)
            {
                throw new Exception("Data Empty!");
            }

            foreach (Match match in first)
            {
                string omschrijving = MatchRegex.GetMatch(subpatternomschrijving, match.Groups[1].Value);
                string linkUrl = MatchRegex.GetMatch(subpatternlink, match.Groups[1].Value);
                string prijs = MatchRegex.GetMatch(subpatternprijs, match.Groups[1].Value);
                string picUrl = MatchRegex.GetMatch(picurl, match.Groups[1].Value);
                int prijsItem = MatchRegex.StripPrijsToDouble(prijs, linkUrl);

                if (omschrijving == String.Empty)
                    throw new Exception("omschrijving Empty!");
                if (linkUrl == String.Empty)
                    throw new Exception("linkurl Empty!");
                if (prijs == String.Empty)
                    throw new Exception("prijs Empty!");
                if (picUrl == String.Empty)
                    throw new Exception("picurl Empty!");
            }

            //return response;
            Console.WriteLine(first);
        }

        [TestMethod]
        public void TestMatchRegexDataSpecsAlternate()
        {
            String pattern = "<div class=\"moreInfos\">(.*?)informatie";
            
            String url = "http://www.alternate.nl/ASRock/EP2C612D8-2T8R-socket-2011-3-moederbord/html/product/1164424?tk=7&lk=13777";
            Regex firstPattern = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string htmlText = ReadlUrl.GetResponse(url);

            MatchCollection first = firstPattern.Matches(htmlText);

            if (first.Count == 0)
            {
                throw new Exception("Data Empty!");
            }

            //return response;
            Console.WriteLine(first);
        }

        [TestMethod]
        public void TestMatchRegexFirstPatternInformatique()
        {
            String subpatternomschrijving = "<div id=\"title\">(.*?)</a></div>";
            String subpatternlink = "<a href=\"http://www.informatique.nl(.*?)\"><img src=\"";
            String subpatternprijs = "<div id=\"price\">(.*?)<";
            String picurl = "<img src=\"http://img.informatique.nl(.*?)\" /></a>";
            String pattern = "<div id=\"image\">(.*?)<div id=\"image\">";
            String url = "http://www.informatique.nl/?m=usl&g=726&view=6&&sort=pop&pl=28";
            Regex firstPattern = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string htmlText = ReadlUrl.GetResponse(url);

            MatchCollection first = firstPattern.Matches(htmlText);

            if (first.Count == 0)
            {
                throw new Exception("Data Empty!");
            }

            foreach (Match match in first)
            {
                string omschrijving = MatchRegex.GetMatch(subpatternomschrijving, match.Groups[1].Value);
                string linkUrl = MatchRegex.GetMatch(subpatternlink, match.Groups[1].Value);
                string prijs = MatchRegex.GetMatch(subpatternprijs, match.Groups[1].Value);
                string picUrl = MatchRegex.GetMatch(picurl, match.Groups[1].Value);
                int prijsItem = MatchRegex.StripPrijsToDouble(prijs, linkUrl);

                if (omschrijving == String.Empty)
                    throw new Exception("omschrijving Empty!");
                if (linkUrl == String.Empty)
                    throw new Exception("linkurl Empty!");
                if (prijs == String.Empty)
                    throw new Exception("prijs Empty!");
                if (picUrl == String.Empty)
                    throw new Exception("picurl Empty!");
            }

            //return response;
            Console.WriteLine(first);
        }

        [TestMethod]
        public void TestMatchRegexDataSpecsInformatique()
        {
            String pattern = "<table id=\"details\" class=\"specs left\">(.*?)</div>";
            String url = "http://www.informatique.nl/201169/asrock-x79-extreme6.html";
            Regex firstPattern = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string htmlText = ReadlUrl.GetResponse(url);

            MatchCollection first = firstPattern.Matches(htmlText);

            if (first.Count == 0)
            {
                throw new Exception("Data Empty!");
            }

            //return response;
            Console.WriteLine(first);
        }

        [TestMethod]
        public void TestReadConfigApi()
        {
            string url = Config.configUrl;
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
                            
                        }
                    }
                }
            }

            if (items == null)
                throw new Exception("Data Empty!");
            Console.WriteLine(items);
        }
    }
}
