# DirectoryConvert
C# Class to convert Distinguished Names to Canonical Names and back

So I'm new to C#, don't bully me. I had a PowerShell snippet laying around (I don't remember where it came from and will happily give credit to anyone that may recognize this) and decided to rewrite it in C# for a project I'm working on .

This mess:
```C#
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
}
```
Will produce this:
> Original distinguished name: CN=James Murdock,OU=Disabled Users,OU=GOG,DC=contoso,DC=local

> Converted Canonical Name: contoso.local/GOG/Disabled Users/James Murdock

> Converted Canonical Name for User: CN=James Murdock,OU=Disabled Users,OU=GOG,DC=contoso,DC=local

> Converted Canonical Name for OU: OU=,OU=Disabled Users,OU=GOG,DC=contoso,DC=local
