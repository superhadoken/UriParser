using System;

namespace UrlParser.Services
{
    public class FooBarPrinter : IPrinter
    {
        public string SystemName => "foo-bar-rule";
        public void Print(object uriModel)
        {
            Console.WriteLine((string)uriModel);
            Console.WriteLine();
        }
    }
}
