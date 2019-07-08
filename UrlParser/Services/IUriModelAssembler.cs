using UrlParser.Model;

namespace UrlParser.Services
{
    public interface IUriModelAssembler
    {
        UriModel Assemble(string inputUri);
    }
}
