using FluentAssertions;
using WeatherBots.Utils;
using Xunit;


namespace WeatherBots.Tests
{
    public class CompletenessCheckerTests
    {
        [Theory]
        [InlineData("{", typeof(JsonCompletenessChecker))]
        [InlineData("[", typeof(JsonCompletenessChecker))]
        [InlineData("<root>", typeof(XmlCompletenessChecker))]
        public void Factory_ReturnsCorrectCheckerType(string firstLine, Type expected)
        {
            var checker = CompletenessCheckerFactory.CreateChecker(firstLine);
            checker.Should().BeOfType(expected);
        }


        [Fact]
        public void JsonCompletenessChecker_CountsBracesCorrectly()
        {
            var checker = new JsonCompletenessChecker();
            checker.Consume("{");
            checker.Consume("}");
            checker.IsComplete.Should().BeTrue();
        }


        [Fact]
        public void XmlCompletenessChecker_DetectsClosingTag()
        {
            var checker = new XmlCompletenessChecker();
            checker.Consume("<root>");
            checker.Consume("</root>");
            checker.IsComplete.Should().BeTrue();
        }

        [Fact]
        public void JsonCompletenessChecker_ShouldHandleNestedBracesAndBrackets()
        {
            var checker = new JsonCompletenessChecker();
            checker.Consume("{ [ {} ] }");
            checker.IsComplete.Should().BeTrue();
        }

        [Fact]
        public void JsonCompletenessChecker_ShouldHandleIncompleteJson()
        {
            var checker = new JsonCompletenessChecker();
            checker.Consume("{ [ {} ");
            checker.IsComplete.Should().BeFalse();
        }

        [Fact]
        public void XmlCompletenessChecker_ShouldDetectNestedTags()
        {
            var checker = new XmlCompletenessChecker();
            checker.Consume("<root>");
            checker.Consume("<child>");
            checker.Consume("</child>");
            checker.Consume("</root>");
            checker.IsComplete.Should().BeTrue();
        }

        [Fact]
        public void XmlCompletenessChecker_ShouldRemainIncompleteWithoutClosingTag()
        {
            var checker = new XmlCompletenessChecker();
            checker.Consume("<root>");
            checker.IsComplete.Should().BeFalse();
        }

    }
}