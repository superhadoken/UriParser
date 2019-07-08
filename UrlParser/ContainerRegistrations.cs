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
            Bind<IMatchingRules>().To<MatchingRules.MatchingRules>();
            
            // Services
            Bind<IUriModelAssembler>().To<UriModelAssembler>();
            Bind<IUriPrinter>().To<UriPrinter>();
        }
    }
}
