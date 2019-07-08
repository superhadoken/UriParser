using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UrlParser.MatchingRules
{
    public class MatchingRules : IMatchingRules
    {
        // RFC 3986 - URI Generic Syntax - Berners-Lee, et al.
        private const string UriGroupMatch = @"^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\?([^#]*))?(#(.*))?";
        private readonly Regex _matcher;

        public MatchingRules()
        {
            /* This matcher always returns 9 groups which we can then check if each is empty
             *
             *  scheme    = $2
                authority = $4
                path      = $5
                query     = $7
                fragment  = $9
             */
            _matcher = new Regex(UriGroupMatch);
        }

        public bool IsValid(string uri)
        {
            var uriValidityRegex = new Regex(@"\w+:(\/?\/?)[^\s]+");

            return uriValidityRegex.IsMatch(uri);
        }

        public string GetScheme(string uri)
        {
            var match = _matcher.Match(uri.ToLower());

            return match.Groups[2].ToString();
        }

        public string GetAuthority(string uri)
        {
            var match = _matcher.Match(uri.ToLower());

            return match.Groups[4].ToString();
        }

        public string GetFragment(string uri)
        {
            var match = _matcher.Match(uri.ToLower());

            return match.Groups[9].ToString();
        }

        public IEnumerable<string> GetPathParams(string uri)
        {
            var match = _matcher.Match(uri.ToLower());

            // Paths split by '/'
            var parameters = match.Groups[5].Value.Split("/", StringSplitOptions.RemoveEmptyEntries).ToList();

            return match.Success && parameters.Any() ? parameters : new List<string>();
        }

        public IEnumerable<string> GetQueryParams(string uri)
        {
            var match = _matcher.Match(uri.ToLower());
            
            if (!match.Success || string.IsNullOrEmpty(match.Groups[7].Value))
                return new List<string>();

            // Queries are split by '&'
            var paramMatcher = match.Groups[7].Value.Split("&", StringSplitOptions.RemoveEmptyEntries);

            // Do we actually have any parameters?
            return !paramMatcher.Any() ? new List<string>() : paramMatcher.ToList();
        }

        public string GetHost(string uri)
        {
            // Assumption made that host is authority with userinfo and port stripped out
            var colonDelimitedAuthority = SplitOnPort(uri);

            if (!colonDelimitedAuthority.Any())
                return string.Empty;

            return colonDelimitedAuthority.First().Split("@", StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
        }

        public string GetPort(string uri)
        {
            var colonDelimitedAuthority = SplitOnPort(uri);

            // If we don't have at least two results from our split, assume there was no colon
            return (!colonDelimitedAuthority.Any() || colonDelimitedAuthority.Count < 2) ? string.Empty : colonDelimitedAuthority.LastOrDefault();

        }

        private IList<string> SplitOnPort(string uri)
        {
            // Assumption made that our port is the final section of the authority after the colon
            var authority = GetAuthority(uri);
            return string.IsNullOrEmpty(authority) ? new List<string>() : authority.Split(":", StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
