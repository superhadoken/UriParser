namespace UrlParser.MatchingRules
{
    public class FooBarMatchingRule : IMatchingRules
    {
        public string SystemName => "foo-bar-rule";

        public bool Applies(string uri)
        {
            return uri.Contains("foobar");
        }

        public T BuildModel<T>(string uri)
        {
            return (T)(object)"Hello World";
        }
    }
}
