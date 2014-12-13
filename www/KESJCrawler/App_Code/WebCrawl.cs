﻿using System;
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
                //Use Regex to get match for first block
                MatchCollection firstMatch = MatchRegex.GetFirstMatch(item.pattern, item.url);

                foreach (Match match in firstMatch)
                {
                    string omschrijving = MatchRegex.GetMatch(item.subpatternomschrijving, match.Groups[1].Value);
                    string linkUrl = MatchRegex.GetMatch(item.subpatternlink, match.Groups[1].Value);
                    string prijs = MatchRegex.GetMatch(item.subpatternprijs, match.Groups[1].Value);
                    string picUrl = MatchRegex.GetMatch(item.picurl, match.Groups[1].Value);
                    int prijsItem = MatchRegex.StripPrijsToDouble(prijs);

                    //Set url for component
                    string urlComponent = item.provider + linkUrl;

                    //strip data omschrijving
                    omschrijving = MatchRegex.StripDataOms(omschrijving);
                    //get the specific specs from componentUrl
                    string dataSpecs = MatchRegex.GetComponentMatch(item.subpatterndata, urlComponent);
                    //strip dataspecs
                    dataSpecs = MatchRegex.StripData(dataSpecs);
                    // Add to list for posting to API
                    items.Add(new itemspost { omschrijving = omschrijving, category = item.category, subcategory = item.subcategory, linkurl = linkUrl, prijs = prijsItem, provider = item.provider, picurl=picUrl, dataspecs = dataSpecs });

                }

            }

            return items;
        }
    }
}