using Ninject.Modules;
using UrlParser.MatchingRules;
using UrlParser.Services;

namespace UrlParser
{
    public class ContainerRegistrations : NinjectModule
    {
        public override void Load()
        {
            // Matching Rules
            Bind<IMatchingRules>().To<UriMatchingRule>();
            Bind<IMatchingRules>().To<FooBarMatchingRule>();
            Bind<IMatchingRuleResolver>().To<MatchingRuleResolver>();
            
            // Services
            Bind<IPrinter>().To<UriPrinter>();
            Bind<IPrinter>().To<FooBarPrinter>();
        }
    }
}
