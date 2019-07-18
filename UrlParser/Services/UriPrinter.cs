using System;
using UrlParser.MatchingRules;

namespace UrlParser.Services
{
    public class UriPrinter : IPrinter
    {
        public string SystemName => "uri-breakdown-rule";
        private const string Undefined = "N/A";

        public void Print(object uriModel)
        {
            var typedUriModel = (UriModel)uriModel;
            if (uriModel == null)
            {
                Console.WriteLine($"Not a valid URI - {typedUriModel.Uri}");
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"URI - {CheckForEmpty(typedUriModel.Uri)}");
            Console.WriteLine($"Scheme - {CheckForEmpty(typedUriModel.Scheme)}");
            Console.WriteLine($"Authority - {CheckForEmpty(typedUriModel.Authority)}");
            Console.WriteLine($"Host - {CheckForEmpty(typedUriModel.Host)}");
            Console.WriteLine($"Port - {CheckForEmpty(typedUriModel.Port)}");
            Console.WriteLine($"Path - {CheckForEmpty(string.Join(", ", typedUriModel.PathParams))}");
            Console.WriteLine($"Query - {CheckForEmpty(string.Join(", ", typedUriModel.QueryParams))}");
            Console.WriteLine($"Fragment - {CheckForEmpty(typedUriModel.Fragment)}");
            Console.WriteLine();
        }

        private string CheckForEmpty(string segment)
        {
            return string.IsNullOrEmpty(segment) ? Undefined : segment;
        }
    }
}
