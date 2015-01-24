using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using KESJCrawler.App_Code;
using System.Text;
using System.Web.Hosting;

/*
  Website crawler.aspx om crawler op afstand te starten
  In opdracht van Hogeschool Rotterdam module : INFPRJ2110
  S. van Staden - 22-01-2015 - studentnr:0883388
*/


namespace KESJCrawler
{
    public partial class Crawler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Server.ScriptTimeout = 3600;
            
            ////Start Crawling
            try
            {
                PostJson getPostData = new PostJson();
                String result = getPostData.PostJsonWebCrawlToApi();
            
                string today = DateTime.Today.ToString();
                //Log if succeeded
                LogFile.LogMe("Post Succeed", Config.Config.postUrl);
                //Return response from API
                Response.Write(result);
            
            }
            catch (Exception exc)
            {
                LogFile.LogMe(exc.Message, "unknown");
            }
            finally
            {
            
            }    
            
        }
    }
}