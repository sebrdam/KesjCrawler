﻿using System;
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


namespace KESJCrawler
{
    public partial class Crawler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Start Crawling
            try
            {
                PostJson getPostData = new PostJson();
                String result = getPostData.PostJsonWebCrawlToApi();
            
                //Return response from API
                Response.Write(result);
            
            }
            catch (Exception exc)
            {
                LogFile.LogError(exc.Message, "unknown");
            }
            finally
            {

            }

            


        }
    }
}