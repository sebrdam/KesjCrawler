using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace KESJCrawler.App_Code
{
    public class WebCrawl
    {
        List<itemspost> items = new List<itemspost>();


        public List<itemspost> CrawlWebsite()
        {
            // Get the config file to read definitions
            List<itemsconfig> config = ReadConfig.getConfigList();

            foreach (var item in config)
            {
                MatchCollection firstmatch = MatchRegex.GetFirstMatch(item.pattern, item.url);

                foreach (Match match in firstmatch)
                {
                    string omschrijving = MatchRegex.GetMatch(item.subpatternomschrijving, match.Groups[1].Value);
                    string linkurl = MatchRegex.GetMatch(item.subpatternlink, match.Groups[1].Value);
                    string prijs = MatchRegex.GetMatch(item.subpatternprijs, match.Groups[1].Value);
                    double prijsitem = MatchRegex.StripPrijsToDouble(prijs);


                    items.Add(new itemspost { omschrijving = omschrijving, category = item.category, subcategory = item.subcategory, linkurl = linkurl, prijs = prijsitem, provider = item.provider });

                }

            }

            return items;
        }
    }
}