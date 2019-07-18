namespace UrlParser.MatchingRules
{
    public interface IMatchingRules
    {
        string SystemName { get; }
        bool Applies(string uri);
        T BuildModel<T> (string uri);
    }
}
