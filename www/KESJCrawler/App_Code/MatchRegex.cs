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
            Regex firstPattern = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string htmlText = ReadlUrl.GetResponse(url);

            MatchCollection first = firstPattern.Matches(htmlText);

            return first;

        }

        public static string GetComponentMatch(string pattern, string url)
        {
            Regex firstPattern = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string htmlText = ReadlUrl.GetResponse(url);

            Match first = firstPattern.Match(htmlText);

            string match1 = first.Groups[1].Value;

            return match1;

        }

        public static string GetMatch(string pattern, string text)
        {
            Regex firstPattern = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Match first = firstPattern.Match(text);

            string match = first.Groups[1].Value;

            return match;

        }

        public static int StripPrijsToDouble(string text, string url)
        {
            string replaced = Regex.Replace(text, "-", "00");
            replaced = Regex.Replace(replaced, @"[^0-9€]", "");
            //int prijs = Convert.ToDouble(replaced);
            int prijs = Convert.ToInt32(replaced);

            return prijs;
        }

        public static string StripData(string data)
        {
            string dataSpecs = Regex.Replace(data, "<.*?>", "-");
            Regex pattern = new Regex("[\"'!<>(),\t\r®.;µ ]");
            dataSpecs = pattern.Replace(dataSpecs, "");

            return dataSpecs;
        }

        public static string StripDataOms(string data)
        {
            string dataSpecs = Regex.Replace(data, "<.*?>", "");
            Regex pattern = new Regex("[\"'!<>(),\t\r®.;µ]");
            dataSpecs = pattern.Replace(dataSpecs, "");

            return dataSpecs;
        }

        public static string StripPicUrl(string data)
        {
            string dataSpecs = Regex.Replace(data, "<.*?>", "");
            Regex pattern = new Regex("[()]");
            dataSpecs = pattern.Replace(dataSpecs, "");

            return dataSpecs;
        }

    }
}