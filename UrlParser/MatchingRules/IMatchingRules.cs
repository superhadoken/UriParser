using System.Collections.Generic;

namespace UrlParser.MatchingRules
{
    public interface IMatchingRules
    {
        bool IsValid(string uri);
        string GetScheme(string uri);
        string GetAuthority(string uri);
        string GetHost(string uri);
        string GetPort(string uri);
        string GetFragment(string uri);
        IEnumerable<string> GetPathParams(string uri);
        IEnumerable<string> GetQueryParams(string uri);
    }
}
