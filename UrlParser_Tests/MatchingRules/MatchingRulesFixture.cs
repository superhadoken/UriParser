using Should;
using System.Linq;
using UrlParser.MatchingRules;
using Xunit;

namespace UrlParser_Tests.MatchingRules
{
    public class MatchingRulesFixture
    {
        private readonly UriMatchingRule _sut;

        public MatchingRulesFixture()
        {
            _sut = new UriMatchingRule();
        }

        [Fact]
        public void AppliesCorrectlyAssesUri()
        {
            // Arrange
            var validUri = "http://www.google.com";
            var invalidUri = "www.google.com";

            // Act
            var isValidResult = _sut.Applies(validUri);
            var isInvalidResult = _sut.Applies(invalidUri);

            // Assert
            isValidResult.ShouldBeTrue();
            isInvalidResult.ShouldBeFalse();
        }

        [Fact]
        public void MatchingSchemeReturnsCorrectValue()
        {
            // Arrange
            var uri = "http://www.google.com";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.ShouldBeType<UriModel>();
            result.Scheme.ShouldEqual("http");
        }

        [Fact]
        public void MatchingSchemeHandlesNoMatch()
        {
            // Arrange
            var uri = "sdjkfshfkasjhkjh";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.Scheme.ShouldBeEmpty();
        }

        [Fact]
        public void MatchingAuthorityReturnsCorrectValue()
        {
            // Arrange
            var uri = "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.Authority.ShouldEqual("john.doe@www.example.com:123");
        }

        [Fact]
        public void MatchingAuthorityHandlesNoMatch()
        {
            // Arrange
            var uri = "notexttomatch";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.Authority.ShouldBeEmpty();
        }

        [Fact]
        public void MatchingPathsReturnsCorrectResults()
        {
            // Arrange
            var uri = "https://memegenerator.net/img/images/200x/42.jpg";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.PathParams.ShouldNotBeEmpty();
            result.PathParams.Count().ShouldEqual(4);
            var resultList = result.PathParams.ToList();
            resultList[0].ShouldEqual("img");
            resultList[1].ShouldEqual("images");
            resultList[2].ShouldEqual("200x");
            resultList[3].ShouldEqual("42.jpg");
        }

        [Fact]
        public void MatchingQueryReturnsCorrectResults()
        {
            // Arrange
            var uri = "https://www.google.com/search?q=google&rlz=1C1CHBF_enGB854GB854&oq=google&aqs=chrome..69i57j0l2j69i60j69i59j35i39.831j0j4&sourceid=chrome&ie=UTF-8";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.QueryParams.ShouldNotBeEmpty();
            result.QueryParams.Count.ShouldEqual(6);
            result.QueryParams["q"].ShouldEqual("google");
            result.QueryParams["rlz"].ShouldEqual("1c1chbf_engb854gb854");
            result.QueryParams["oq"].ShouldEqual("google");
            result.QueryParams["aqs"].ShouldEqual("chrome..69i57j0l2j69i60j69i59j35i39.831j0j4");
            result.QueryParams["sourceid"].ShouldEqual("chrome");
            result.QueryParams["ie"].ShouldEqual("utf-8");
        }

        [Fact]
        public void MatchingQueryHandlesNoMatch()
        {
            // Arrange
            var uri = "www.google.com/searching/index.html";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.QueryParams.ShouldBeEmpty();
        }

        [Fact]
        public void MatchingHostsReturnsCorrectResults()
        {
            // Arrange
            var uri = "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.Host.ShouldNotBeEmpty();
            result.Host.ShouldEqual("www.example.com");
        }

        [Fact]
        public void MatchingFragmentReturnsCorrectResults()
        {
            // Arrange
            var uri = "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.Fragment.ShouldNotBeEmpty();
            result.Fragment.ShouldEqual("top");
        }

        [Fact]
        public void MatchingPortReturnsCorrectResults()
        {
            // Arrange
            var uri = "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.Port.ShouldNotBeEmpty();
            result.Port.ShouldEqual("123");
        }

        [Fact]
        public void MatchingPortAvoidsFalsePositive()
        {
            // Arrange 
            var uri = "http://www.google.com";

            // Act
            var result = _sut.BuildModel<UriModel>(uri);

            // Assert
            result.Port.ShouldBeEmpty();
        }
    }
}
