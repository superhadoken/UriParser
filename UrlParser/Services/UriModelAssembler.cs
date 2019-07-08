using UrlParser.MatchingRules;
using UrlParser.Model;

namespace UrlParser.Services
{
    public class UriModelAssembler : IUriModelAssembler
    {
        private readonly IMatchingRules _matchingRules;

        public UriModelAssembler(IMatchingRules matchingRules)
        {
            _matchingRules = matchingRules;
        }

        public UriModel Assemble(string inputUri)
        {
            if (!_matchingRules.IsValid(inputUri))
                return new UriModel
                {
                    Uri = inputUri,
                    IsValid = false
                };

            var model = new UriModel
            {
                Uri = inputUri,
                IsValid = true,
                Scheme = _matchingRules.GetScheme(inputUri),
                Host = _matchingRules.GetHost(inputUri),
                Port = _matchingRules.GetPort(inputUri),
                Authority = _matchingRules.GetAuthority(inputUri),
                PathParams = _matchingRules.GetPathParams(inputUri),
                QueryParams = _matchingRules.GetQueryParams(inputUri),
                Fragment = _matchingRules.GetFragment(inputUri)
            };
            
            return model;
        }
    }
}
