using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

/*
  Log alle activiteiten voor crawler
  In opdracht van Hogeschool Rotterdam module : INFPRJ2110
  S. van Staden - 22-01-2015 - studentnr:0883388
*/

namespace KESJCrawler.App_Code
{
    public class LogFile
    {
        private static XmlDocument log = new XmlDocument();

        private static string path;

        public void SetPath(string getPath)
        {
            path = getPath;
        }


        public static void LogMe(string error, string url)
        {

            //load the logfile from config and log errors
            log.Load(Config.Config.logFile);

            string date = DateTime.Now.ToString();

            var elog = (XmlElement)log.DocumentElement.AppendChild(log.CreateElement("logs"));
            elog.SetAttribute("date", date);
            elog.AppendChild(log.CreateElement("url")).InnerText = url;
            elog.AppendChild(log.CreateElement("log")).InnerText = error;
            log.Save(Config.Config.logFile);

        }
    }
}