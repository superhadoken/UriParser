namespace UrlParser.Services
{
    public interface IPrinter
    {
        string SystemName { get; }
        void Print(object uriModel);
    }
}
