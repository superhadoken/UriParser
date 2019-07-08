using System;
using UrlParser.Model;

namespace UrlParser.Services
{
    public class UriPrinter : IUriPrinter
    {
        private const string Undefined = "N/A";

        public void Print(UriModel uriModel)
        {
            if (!uriModel.IsValid)
            {
                Console.WriteLine($"Not a valid URI - {uriModel.Uri}");
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"URI - {CheckForEmpty(uriModel.Uri)}");
            Console.WriteLine($"Scheme - {CheckForEmpty(uriModel.Scheme)}");
            Console.WriteLine($"Authority - {CheckForEmpty(uriModel.Authority)}");
            Console.WriteLine($"Host - {CheckForEmpty(uriModel.Host)}");
            Console.WriteLine($"Port - {CheckForEmpty(uriModel.Port)}");
            Console.WriteLine($"Path - {CheckForEmpty(string.Join(", ", uriModel.PathParams))}");
            Console.WriteLine($"Query - {CheckForEmpty(string.Join(", ", uriModel.QueryParams))}");
            Console.WriteLine($"Fragment - {CheckForEmpty(uriModel.Fragment)}");
            Console.WriteLine();
        }

        private string CheckForEmpty(string segment)
        {
            return string.IsNullOrEmpty(segment) ? Undefined : segment;
        }
    }
}
