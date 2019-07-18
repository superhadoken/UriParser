using System.Collections.Generic;

namespace UrlParser.Data
{
    /// <summary>
    /// Class is temporary data source - in full library/app we would rely on the DataReader class
    /// </summary>
    public static class Input
    {
        public static IEnumerable<string> Data => new List<string>
        {
            "www.foobar.com",
            "dsjioufskhsauiyfawsiusdhcdxizuyh",
            "http://www.google.com",
            "https://localhost:5000",
            "http://127.0.0.1:8080/",
            "telnet://192.0.2.16:80/",
            "https://alexwohlbruck.github.io/cat-facts/",
            "https://memegenerator.net/img/images/200x/42.jpg",
            "tel:+1-816-555-1212",
            "news:comp.infosystems.www.servers.unix",
            "https://www.youtube.com/results?search_query=test+search",
            "https://http.cat/200",
            "https://en.wikipedia.org/wiki/Uniform_Resource_Identifier#Examples",
            "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top",
            "https://www.google.com/search?q=google&rlz=1C1CHBF_enGB854GB854&oq=google&aqs=chrome..69i57j0l2j69i60j69i59j35i39.831j0j4&sourceid=chrome&ie=UTF-8",
            "http://version1.api.memegenerator.net/Comment_Create?entityName=Instance&entityID=72628355&parentCommentID=&text=first%20post%20best%20post&apiKey=demo",
            "http://version1.api.memegenerator.net//ContentFlag_Create?contentUrl=https://memegenerator.net/John-Doe&reason=personal%20information%20exposed&email=email@domain.com&apiKey=demo",
            "http://version1.api.memegenerator.net//ContentFlag_Create?contentUrl=https%3A%2F%2Fmemegenerator.net%2FJohn-Doe%26reason%3Dpersonal%2520information%2520exposed%26email%3Demail%40domain.com%26apiKey%3Ddemo",
        };
    }
}
