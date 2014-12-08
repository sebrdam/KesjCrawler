using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace KESJCrawler.App_Code
{
    public class MatchRegex
    {

        public static MatchCollection GetFirstMatch(string pattern, string url)
        {
            Regex firstpattern = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string htmlText = ReadlUrl.GetResponse(url);

            MatchCollection first = firstpattern.Matches(htmlText);

            return first;

        }

        public static string GetMatch(string pattern, string text)
        {
            Regex firstpattern = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Match first = firstpattern.Match(text);

            string match = first.Groups[1].Value;

            return match;

        }

        public static double StripPrijsToDouble(string text)
        {
            string replaced = Regex.Replace(text, @"[^0-9€,]", "");
            double prijs = Convert.ToDouble(replaced);

            return prijs;
        }
    }
}