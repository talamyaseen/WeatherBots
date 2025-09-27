using System;
using System.IO;
using FluentAssertions;
using WeatherBots.Utils;
using Xunit;

namespace WeatherBots.Tests
{
    public class InputReaderTests
    {
        [Fact]
        public void ReadDocument_ShouldReturnNull_ForEmptyInput()
        {
            Console.SetIn(new StringReader("\n"));
            var result = InputReader.ReadDocument();
            result.Should().BeNull();
        }

        [Fact]
        public void ReadDocument_ShouldReturnNull_ForWhitespaceOnly()
        {
            Console.SetIn(new StringReader("   \n"));
            var result = InputReader.ReadDocument();
            result.Should().BeNull();
        }

        [Fact]
        public void ReadDocument_ShouldReadMultiLineJsonDocument()
        {
            var input = "{\n\"Location\": \"A\",\n\"Temperature\": 10,\n\"Humidity\": 20\n}";
            Console.SetIn(new StringReader(input));
            var doc = InputReader.ReadDocument();
            doc.Should().Contain("\"Location\": \"A\"");
            doc.Should().Contain("\"Temperature\": 10");
            doc.Should().Contain("\"Humidity\": 20");
        }

        [Fact]
        public void ReadDocument_ShouldReadMultiLineXmlDocument()
        {
            var input = "<root>\n<child>data</child>\n</root>";
            Console.SetIn(new StringReader(input));
            var doc = InputReader.ReadDocument();
            doc.Should().Contain("<root>");
            doc.Should().Contain("<child>");
            doc.Should().Contain("</root>");
        }

        [Fact]
        public void ReadDocument_ShouldStopAtCompleteJson()
        {
            var input = "{\n\"Location\": \"B\"\n}\nExtraLine\n";
            Console.SetIn(new StringReader(input));
            var doc = InputReader.ReadDocument();
            doc.Should().Contain("\"Location\": \"B\"");
            doc.Should().NotContain("ExtraLine");
        }

        [Fact]
        public void ReadDocument_ShouldStopAtCompleteXml()
        {
            var input = "<root>\n<data>1</data>\n</root>\nExtraLine\n";
            Console.SetIn(new StringReader(input));
            var doc = InputReader.ReadDocument();
            doc.Should().Contain("<root>");
            doc.Should().NotContain("ExtraLine");
        }

        [Fact]
        public void ReadDocument_ShouldThrow_ForUnsupportedFormat()
        {
            var input = "unsupported format line\n";
            Console.SetIn(new StringReader(input));
            Action act = () => InputReader.ReadDocument();
            act.Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void ReadDocument_ShouldHandleNestedJson()
        {
            var input = "{\n\"Outer\": { \"Inner\": { \"Value\": 123 } }\n}";
            Console.SetIn(new StringReader(input));
            var doc = InputReader.ReadDocument();
            doc.Should().Contain("\"Outer\"");
            doc.Should().Contain("\"Inner\"");
            doc.Should().Contain("\"Value\": 123");
        }

        [Fact]
        public void ReadDocument_ShouldHandleNestedXml()
        {
            var input = "<root>\n<parent>\n<child>value</child>\n</parent>\n</root>";
            Console.SetIn(new StringReader(input));
            var doc = InputReader.ReadDocument();
            doc.Should().Contain("<root>");
            doc.Should().Contain("<parent>");
            doc.Should().Contain("<child>value</child>");
        }
    }
}
