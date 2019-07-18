using Ninject;
using System.Linq;
using System.Reflection;
using UrlParser.Data;
using UrlParser.MatchingRules;
using UrlParser.Services;

namespace UrlParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var rules = kernel.GetAll<IMatchingRules>().ToList();
            var printerServices = kernel.GetAll<IPrinter>().ToList();
            var ruleResolver = kernel.Get<IMatchingRuleResolver>();

            foreach (var uri in Input.Data)
            {
                var matchedRules = ruleResolver.ResolveRules(rules, uri);

                foreach (var (ruleSystemName, objectModel) in matchedRules)
                {
                    foreach (var printerService in printerServices)
                    {
                        if (printerService.SystemName == ruleSystemName)
                            printerService.Print(objectModel);
                    }
                }
            }
        }

    }
}
