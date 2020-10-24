using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Shamer_4001
{
    class Class1
    {
        public static void Main(String[] args)
        {

            WebClient wc = new WebClient();
            //wc.QueryString.Add("type", "army");
            //wc.QueryString.Add("role", "all");
            //wc.QueryString.Add("country", "all");

            
            string res = wc.DownloadString("http://thunderskill.com/en/stat/DEFYN/vehicles/r");
            //System.IO.File.WriteAllText(@"op.txt", res);

            var doc = new HtmlDocument();
            doc.LoadHtml(res);

            //var trs = doc.DocumentNode.Descendants("tr");

            //var tables = doc.DocumentNode.Descendants("table");
            //Console.WriteLine($"{tables.Count()}");

            //var trs_arr = trs.ToArray();

            //GET ALL TABLES IN HTML, SHOULD BE 1 FOR PLANES AND 1 FOR TANKS. GET ALL TRS FROM SELECTED TABLE (INDEX 0 FOR PLANES, 1 FOR TANKS)
            var trs_arr = doc.DocumentNode.Descendants("table").ToArray()[1].Descendants("tr").ToArray();
            
            string name;
            string tier;
            HtmlNode[] lis_arr;
            HashSet<string> roles = new HashSet<string>();

            for (int i = 1; i < trs_arr.Length; i++)
            {
                try { 
                    name = trs_arr[i]
                        .Elements("td")
                        .ToArray()[1]
                        .Element("span")
                        .InnerText;

                    tier = trs_arr[i]
                        .Element("td")
                        .InnerText;

                    lis_arr = trs_arr[i]
                        .Elements("td")
                        .ToArray()[1]
                        .Element("div")
                        .Element("ul")
                        .Elements("li")
                        .ToArray();

                    Console.WriteLine(name);
                    Console.WriteLine(tier);
                    Console.WriteLine(trs_arr[i].Attributes["data-role"].Value);
                    roles.Add(trs_arr[i].Attributes["data-role"].Value);

                    HtmlNode[] spans;
                    foreach (var li in lis_arr)
                    {
                        spans = li.Elements("span").ToArray();
                        if (spans.Length != 2) continue;
                        Console.WriteLine($"{spans[0].InnerText} : {spans[1].InnerText}");
                    }

                    Console.WriteLine("----------------");
                }
                catch
                {
                    Console.WriteLine("Borke");
                    for (int z = 0; z < 20; z++) Console.WriteLine();
                    continue;
                }

            }

            foreach (string a in roles) Console.WriteLine(a);

            /*
            var innertext_arr = trs.Select(n => n.Elements("td").Select(e => e.InnerText).ToArray());

            var ct = 0;
            foreach (var tr in innertext_arr) {
                ct++;
                if (ct > 100) break;
                Console.WriteLine();
                try {
                    for (int i = 0; i < tr.Length; i++)
                    {
                        Console.WriteLine($"{ i} : { tr[i].Trim()}");
                    }
                }
                catch {
                    Console.WriteLine("Index Out of BOunds");
                    continue;  
                }
                
            }

            Yak-2 KABB
                1
                fighters all
                Battles : 8
                Respawns : 8
                Victories : 5
                Defeats : 3
                Deaths : 3
                Air frags / battle : 3.3
                Air frags / death : 8.7
                Overall air frags : 26
                Overall ground frags : 0

            Sd.Kfz. 140/1
                1
                light_tank all
                Battles : 2
                Respawns : 2
                Victories : 2
                Defeats : 0
                Deaths : 1
                Overall air frags : 0
                Ground frags / battle : 2.5
                Ground frags / death : 5
                Overall ground frags : 5

            */

        }
    }
}