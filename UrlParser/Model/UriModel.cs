using System.Collections.Generic;

namespace UrlParser.Model
{
    public class UriModel
    {
        public string Uri { get; set; }
        public bool IsValid { get; set; }
        public string Scheme { get; set; }
        public string Authority { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public IEnumerable<string> PathParams { get; set; }
        public IEnumerable<string> QueryParams { get; set; }
        public string Fragment { get; set; }
    }
}