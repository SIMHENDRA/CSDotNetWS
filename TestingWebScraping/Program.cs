using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shamer_4001
{
    class Class1
    {
        public static void Main(String[] args)
        {
            WebClient wc = new WebClient();
            wc.QueryString.Add("type", "aviation");
            wc.QueryString.Add("role", "fighter");
            wc.QueryString.Add("country", "all");
            string res = wc.DownloadString("http://thunderskill.com/en/stat/DEFYN/vehicles/r");
            System.IO.File.WriteAllText(@"op.txt", res);
        }
    }
}