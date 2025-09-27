using FluentAssertions;
using WeatherBots.Models;
using WeatherBots.Parsing;
using Xunit;
using Xunit.Sdk;


namespace WeatherBots.Tests
{
    public class ParserAndParsersTests
    {
        [Fact]
        public void JsonWeatherParser_ShouldParseValidJson()
        {
            var parser = new JsonWeatherParser();
            var json = "{ \"Location\": \"X\", \"Temperature\": 12.5, \"Humidity\": 55 }";


            var ok = parser.TryParse(json, out WeatherData? data);


            ok.Should().BeTrue();
            data.Should().NotBeNull();
            data!.Location.Should().Be("X");
            data.Temperature.Should().BeApproximately(12.5, 0.0001);
            data.Humidity.Should().BeApproximately(55, 0.0001);
        }


        [Fact]
        public void XmlWeatherParser_ShouldParseValidXml()
        {
            var parser = new XmlWeatherParser();
            var xml = "<WeatherData><Location>Y</Location><Temperature>1.0</Temperature><Humidity>10</Humidity></WeatherData>";


            var ok = parser.TryParse(xml, out WeatherData? data);


            ok.Should().BeTrue();
            data.Should().NotBeNull();
            data!.Location.Should().Be("Y");
        }

        [Fact]
        public void XmlWeatherParser_ShouldReturnFalseForInvalidXml()
        {
            var parser = new XmlWeatherParser();
            var ok = parser.TryParse("<WeatherData><Invalid></WeatherData>", out var data);
            ok.Should().BeFalse();
            data.Should().BeNull();
        }

        [Fact]
        public void ParserFactory_ShouldHandleMultiLineJson()
        {
            var multiLineJson = "{\n\"Location\": \"ML\",\n\"Temperature\": 15,\n\"Humidity\": 20\n}";
            var ok = ParserFactory.TryParse(multiLineJson, out var data);
            ok.Should().BeTrue();
            data!.Location.Should().Be("ML");
        }


        [Fact]
        public void ParserFactory_TryParse_TriesAllRegisteredParsers()
        {
            var json = "{ \"Location\": \"Z\", \"Temperature\": 2, \"Humidity\": 3 }";
            var ok = ParserFactory.TryParse(json, out var data);
            ok.Should().BeTrue();
            data.Should().NotBeNull();
        }


        [Fact]
        public void ParserFactory_RegisterParser_AllowsCustomParser()
        {
            // Arrange: register a simple test parser
            var parser = new TestParser();
            ParserFactory.RegisterParser(parser);

            // Act
            var ok = ParserFactory.TryParse("THISTESTPARSERINPUT", out var data);

            // Assert
            ok.Should().BeTrue();
            data!.Location.Should().Be("test");
        }

        private class TestParser : IWeatherParser
        {
            public string Name => "test";
            public bool TryParse(string input, out WeatherData? data)
            {
                data = null;
                if (input == "THISTESTPARSERINPUT")
                {
                    data = new WeatherData { Location = "test", Temperature = 0, Humidity = 0 };
                    return true;
                }
                return false;
            }
        }
    }
}