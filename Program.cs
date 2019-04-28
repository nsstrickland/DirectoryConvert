using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryConvert Converter = new DirectoryConvert();
            string distinguishedname = "CN=James Murdock,OU=Disabled Users,OU=GOG,DC=contoso,DC=local";
            string cn = Converter.ConvertFromDN(distinguishedname);
            string usr = Converter.ConvertFromCanonicalUser(cn);
            string ou = Converter.ConvertFromCanonicalOU(cn.Replace("James Murdock", ""));
            Console.WriteLine("Original distinguished name: {0}", distinguishedname);
            Console.WriteLine("Converted Canonical Name: {0}",cn);
            Console.WriteLine("Converted Canonical Name for User: {0}",usr);
            Console.WriteLine("Converted Canonical Name for OU: {0}",ou);
            Console.Read();
        }
    }
    public class DirectoryConvert
    {
        public string ConvertFromDN(string distinguishedname)
        {
            string cn = "";
            string dc = "";
            List<string> ou = new List<string>();
            foreach (string item in ((distinguishedname.Replace("\\,", "~")).Split(',')))
            {
                switch (item.TrimStart().Substring(0, 2))
                {
                    case "CN":
                        cn = ('/' + item.Replace("CN=", ""));
                        break;
                    case "OU":
                        ou.Add(item.Replace("OU=", ""));
                        ou.Add("/");
                        break;
                    case "DC":
                        dc += (item.Replace("DC=", ""));
                        dc += (".");
                        break;
                }
            }
            string canonicalname = dc.Substring(0, (dc.Length - 1));
            Console.WriteLine(ou.Count());
            for (int i = (ou.Count() - 1); i >= 0; i--)
            {
                canonicalname += ou[i];
            }
            if (distinguishedname.Substring(0, 2) == "CN")
            {
                canonicalname += cn.Replace("~", "\\,");
            }
            return canonicalname;
        }
        public string ConvertFromCanonicalUser(string canonicalname)
        {
            string[] obj = canonicalname.Replace(",", "\\,").Split('/');
            string dn = "CN=" + obj[(obj.Count() - 1)];
            for (int i = (obj.Count() - 2); i >= 1; i--)
            {
                dn += ",OU=" + obj[i];
            }
            foreach (string i in (obj[0].Split('.')))
            {
                dn += ",DC=" + i;
            }
            return dn;
        }
        public string ConvertFromCanonicalOU(string canonicalname)
        {
            string[] obj = canonicalname.Replace(",", "\\,").Split('/');
            string dn = "OU=" + obj[(obj.Count() - 1)];
            for (int i = (obj.Count() - 2); i >= 1; i--)
            {
                dn += ",OU=" + obj[i];
            }
            foreach (string i in (obj[0].Split('.')))
            {
                dn += ",DC=" + i;
            }
            return dn;
        }

    }
}
