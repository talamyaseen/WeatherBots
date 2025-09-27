using System.IO;
using System.Xml.Serialization;
using WeatherBots.Models;

namespace WeatherBots.Parsing
{
    public class XmlWeatherParser : IWeatherParser
    {
        public string Name => "xml";

        public bool TryParse(string input, out WeatherData? data)
        {
            data = null;
            try
            {
                var serializer = new XmlSerializer(typeof(WeatherData));
                using var reader = new StringReader(input);
                var obj = serializer.Deserialize(reader) as WeatherData;
                if (obj != null)
                {
                    data = obj;
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }
    }
}
