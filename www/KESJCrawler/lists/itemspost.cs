using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KESJCrawler
{
    public class itemspost
    {
        public string category { set; get; }
        public string subcategory { set; get; }
        public string linkurl { set; get; }
        public string omschrijving { set; get; }
        public double prijs { set; get; }
        public string provider { set; get; }
    }
}

