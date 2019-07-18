using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlParser.MatchingRules
{
    public interface IMatchingRuleResolver
    {
        IDictionary<string, object> ResolveRules(IEnumerable<IMatchingRules> matchingRules, string inputUri);
    }
}
