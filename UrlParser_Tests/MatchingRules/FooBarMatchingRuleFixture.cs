using Should;
using UrlParser.MatchingRules;
using Xunit;

namespace UrlParser_Tests.MatchingRules
{
    public class FooBarMatchingRuleFixture
    {
        private readonly FooBarMatchingRule _sut;

        public FooBarMatchingRuleFixture()
        {
            _sut = new FooBarMatchingRule();
        }

        [Fact]
        public void AppliesCorrectlyAssesUri()
        {
            // Arrange
            var validUri = "http://www.foobar.com";
            var invalidUri = "www.google.com";

            // Act
            var isValidResult = _sut.Applies(validUri);
            var isInvalidResult = _sut.Applies(invalidUri);

            // Assert
            isValidResult.ShouldBeTrue();
            isInvalidResult.ShouldBeFalse();
        }

        [Fact]
        public void ReturnCorrectObject()
        {
            // Arrange
            var uri = "http://www.foobar.com";

            // Act
            var result = _sut.BuildModel<string>(uri);

            // Assert
            result.ShouldBeType<string>();
            result.ShouldEqual("Hello World");
        }
    }
}
