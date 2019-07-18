using System.Collections.Generic;

namespace UrlParser.MatchingRules
{
    public class MatchingRuleResolver : IMatchingRuleResolver
    {
        /// <summary>
        /// Return a dictionary of applicable matching rules for a given uri
        /// </summary>
        /// <param name="matchingRules"></param>
        /// <param name="inputUri"></param>
        /// <returns></returns>
        public IDictionary<string, object> ResolveRules(IEnumerable<IMatchingRules> matchingRules, string inputUri)
        {
            var modelsToPrint = new Dictionary<string, object>();

            // Have kept this in foreach loop instead of LINQ for readability
            foreach (var matchingRule in matchingRules)
            {
                if (matchingRule.Applies(inputUri))
                {
                    modelsToPrint.Add(matchingRule.SystemName, matchingRule.BuildModel<object>(inputUri));
                }
            }

            return modelsToPrint;
        }
    }
}
