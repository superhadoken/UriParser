using System.Linq;
using Should;
using Xunit;

namespace UrlParser_Tests.MatchingRules
{
    public class MatchingRulesFixture
    {
        private readonly UrlParser.MatchingRules.MatchingRules _sut;

        public MatchingRulesFixture()
        {
            _sut = new UrlParser.MatchingRules.MatchingRules();
        }

        [Fact]
        public void IsValidCorrectlyAssesUri()
        {
            // Arrange
            var validUri = "http://www.google.com";
            var invalidUri = "www.google.com";

            // Act
            var isValidResult = _sut.IsValid(validUri);
            var isInvalidResult = _sut.IsValid(invalidUri);

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
            var result = _sut.GetScheme(uri);

            // Assert
            result.ShouldEqual("http");
        }

        [Fact]
        public void MatchingSchemeHandlesNoMatch()
        {
            // Arrange
            var uri = "sdjkfshfkasjhkjh";

            // Act
            var result = _sut.GetScheme(uri);

            // Assert
            result.ShouldBeEmpty();
        }

        [Fact]
        public void MatchingAuthorityReturnsCorrectValue()
        {
            // Arrange
            var uri = "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top";

            // Act
            var result = _sut.GetAuthority(uri);

            // Assert
            result.ShouldEqual("john.doe@www.example.com:123");
        }

        [Fact]
        public void MatchingAuthorityHandlesNoMatch()
        {
            // Arrange
            var uri = "notexttomatch";

            // Act
            var result = _sut.GetAuthority(uri);

            // Assert
            result.ShouldBeEmpty();
        }

        [Fact]
        public void MatchingPathsReturnsCorrectResults()
        {
            // Arrange
            var uri = "https://memegenerator.net/img/images/200x/42.jpg";

            // Act
            var result = _sut.GetPathParams(uri);

            // Assert
            result.ShouldNotBeEmpty();
            result.Count().ShouldEqual(4);
            var resultList = result.ToList();
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
            var result = _sut.GetQueryParams(uri);

            // Assert
            result.ShouldNotBeEmpty();
            result.Count().ShouldEqual(6);
            var resultList = result.ToList();
            resultList[0].ShouldEqual("q=google");
            resultList[1].ShouldEqual("rlz=1c1chbf_engb854gb854");
            resultList[2].ShouldEqual("oq=google");
            resultList[3].ShouldEqual("aqs=chrome..69i57j0l2j69i60j69i59j35i39.831j0j4");
            resultList[4].ShouldEqual("sourceid=chrome");
            resultList[5].ShouldEqual("ie=utf-8");
        }

        [Fact]
        public void MatchingQueryHandlesNoMatch()
        {
            // Arrange
            var uri = "www.google.com/searching/index.html";

            // Act
            var result = _sut.GetQueryParams(uri);

            // Assert
            result.ShouldBeEmpty();
        }

        [Fact]
        public void MatchingHostsReturnsCorrectResults()
        {
            // Arrange
            var uri = "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top";

            // Act
            var result = _sut.GetHost(uri);

            // Assert
            result.ShouldNotBeEmpty();
            result.ShouldEqual("www.example.com");
        }

        [Fact]
        public void MatchingFragmentReturnsCorrectResults()
        {
            // Arrange
            var uri = "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top";

            // Act
            var result = _sut.GetFragment(uri);

            // Assert
            result.ShouldNotBeEmpty();
            result.ShouldEqual("top");
        }

        [Fact]
        public void MatchingPortReturnsCorrectResults()
        {
            // Arrange
            var uri = "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top";

            // Act
            var result = _sut.GetPort(uri);

            // Assert
            result.ShouldNotBeEmpty();
            result.ShouldEqual("123");
        }

        [Fact]
        public void MatchingPortAvoidsFalsePositive()
        {
            // Arrange 
            var uri = "http://www.google.com";

            // Act
            var result = _sut.GetPort(uri);

            // Assert
            result.ShouldBeEmpty();
        }
    }
}
