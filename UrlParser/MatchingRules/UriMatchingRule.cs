using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UrlParser.MatchingRules
{
    public class UriModel
    {
        public string Uri { get; set; }
        public string Scheme { get; set; }
        public string Authority { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public IEnumerable<string> PathParams { get; set; }
        public IDictionary<string, string> QueryParams { get; set; }
        public string Fragment { get; set; }
    }

    public class UriMatchingRule : IMatchingRules
    {
        // RFC 3986 - URI Generic Syntax - Berners-Lee, et al.
        private const string UriGroupMatch = @"^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\?([^#]*))?(#(.*))?";
        public string SystemName => "uri-breakdown-rule";

        public UriMatchingRule()
        {
        }

        public bool Applies(string uri)
        {
            var uriValidityRegex = new Regex(@"\w+:(\/?\/?)[^\s]+");

            return uriValidityRegex.IsMatch(uri);
        }

        public T BuildModel<T>(string uri)
        {
            /* This matcher always returns 9 groups which we can then check if each is empty
             *
             *  scheme    = $2
                authority = $4
                path      = $5
                query     = $7
                fragment  = $9
             */
            var match = new Regex(UriGroupMatch).Match(uri.ToLower());


            //TODO this is ugly and we lose our Type safety - rework
            return (T)(object)Map(match, uri);
        }

        private UriModel Map(Match match, string input)
        {
            var model = new UriModel
            {
                Uri = input,
                Authority = GetAuthority(match),
                Port = GetPort(match),
                Fragment = GetFragment(match),
                Scheme = GetScheme(match),
                QueryParams = GetQueryParams(match),
                PathParams = GetPathParams(match),
                Host = GetHost(match)
            };

            return model;
        }

        private string GetScheme(Match match)
        {
            return match.Groups[2].ToString();
        }

        private string GetAuthority(Match match)
        {
            return match.Groups[4].ToString();
        }

        private string GetFragment(Match match)
        {
            return match.Groups[9].ToString();
        }

        private IEnumerable<string> GetPathParams(Match match)
        {
            // Paths split by '/'
            var parameters = match.Groups[5].Value.Split("/", StringSplitOptions.RemoveEmptyEntries).ToList();

            return match.Success && parameters.Any() ? parameters : new List<string>();
        }

        private IDictionary<string, string> GetQueryParams(Match match)
        {
            if (!match.Success || string.IsNullOrEmpty(match.Groups[7].Value))
                return new Dictionary<string, string>();

            // Queries are split by '&'
            var paramMatcher = match.Groups[7].Value.Split("&", StringSplitOptions.RemoveEmptyEntries);

            // Do we actually have any parameters?
            return !paramMatcher.Any() ? new Dictionary<string, string>() : ConvertQueryParamsIntoDict(paramMatcher);
        }

        private IDictionary<string, string> ConvertQueryParamsIntoDict(string[] parameters)
        {
            return parameters.Select(parameter => parameter.Split("=")).ToDictionary(split => split[0], split => split[1]);
        }

        private string GetHost(Match match)
        {
            // Assumption made that host is authority with userinfo and port stripped out
            var colonDelimitedAuthority = SplitOnPort(match);

            if (!colonDelimitedAuthority.Any())
                return string.Empty;

            return colonDelimitedAuthority.First().Split("@", StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
        }

        private string GetPort(Match match)
        {
            var colonDelimitedAuthority = SplitOnPort(match);

            // If we don't have at least two results from our split, assume there was no colon
            return (!colonDelimitedAuthority.Any() || colonDelimitedAuthority.Count < 2) ? string.Empty : colonDelimitedAuthority.LastOrDefault();

        }

        private IList<string> SplitOnPort(Match match)
        {
            // Assumption made that our port is the final section of the authority after the colon
            var authority = GetAuthority(match);
            return string.IsNullOrEmpty(authority) ? new List<string>() : authority.Split(":", StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
