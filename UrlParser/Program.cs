using Ninject;
using System.Reflection;
using UrlParser.Data;
using UrlParser.Services;

namespace UrlParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var uriBuilder = kernel.Get<IUriModelAssembler>();
            var uriPrinterService = kernel.Get<IUriPrinter>();

            foreach (var uri in Input.Data)
            {
                var uriModel = uriBuilder.Assemble(uri);
                uriPrinterService.Print(uriModel);
            }
        }
    }
}
