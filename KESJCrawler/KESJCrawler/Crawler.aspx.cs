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


namespace KESJCrawler
{
    public partial class Crawler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            string bas = "test";
            //test the output
            Response.Write(bas);

        }
    }
}