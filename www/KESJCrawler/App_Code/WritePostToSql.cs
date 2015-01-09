using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KESJCrawler.App_Code
{
    public class WritePostToSql
    {
         //Write post to file for logging and testing and make sql 

        public static void WriteToFile(string post)
        {
            string myFileName = String.Format("{0}__{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), "post.sql");
            string myFullPath = Path.Combine("C:\\bas", myFileName);

            string sql = "INSERT INTO `rawdata` (`category`, `subcategory`, `linkurl`, `omschrijving`, `prijs`, `provider`, `dataspecs`, `picurl`) VALUES";
            System.IO.StreamWriter file = new System.IO.StreamWriter(myFullPath);
            dynamic deserializedValue = JsonConvert.DeserializeObject(post);
            var values = deserializedValue["data"];
            int count = values.Count;

            file.WriteLine(sql);

            for (int i = 0; i < values.Count; i++)
            {
                int n = i + 1;
                file.WriteLine("('" + values[i].category + "','" + values[i].subcategory + "','" + values[i].linkurl + "','" + values[i].omschrijving + "','" + values[i].prijs + "','" + values[i].provider + "','" + values[i].dataspecs + "','" + values[i].picurl + "')");
                if (count == n)
                    file.WriteLine(";");
                else
                    file.WriteLine(",");
            }
            file.Close();
            
        }
    }
}