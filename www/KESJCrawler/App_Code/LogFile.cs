using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace KESJCrawler.App_Code
{
    public class LogFile
    {
        private static XmlDocument log = new XmlDocument();


        public static void LogError(string error, string url)
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