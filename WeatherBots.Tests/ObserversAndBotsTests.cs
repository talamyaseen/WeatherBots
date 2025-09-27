using System;
using Xunit;


namespace WeatherBots.Tests
{
	public class ObserversAndBotsTests
	{
		[Fact]
		public void WeatherStation_AttachDetach_Publish_CallsObseverUpdate()
		{
			var station = new WeatherStation();
			var mockObs = new Mock<IWeatherObserver>();
			var data = new WeatherData { Location = "L", Temperature = 1, Humidity = 2 };


			station.Attach(mockObs.Object);
			station.Publish(data);


			mockObs.Verify(x => x.Update(It.IsAny<WeatherData>()), Times.Once);


			station.Detach(mockObs.Object);
			station.Publish(data);
			mockObs.Verify(x => x.Update(It.IsAny<WeatherData>()), Times.Once, "should not be called after detach");
		}


		[Fact]
		public void RainBot_WhenHumidityAboveThreshold_WritesToConsole()
		{
			var cfg = new Config.BotConfig { Enabled = true, HumidityThreshold = 50, Message = "raining" };
			var bot = new RainBot(cfg);
			var data = new WeatherData { Location = "x", Temperature = 10, Humidity = 60 };


			var sw = new StringWriter();
			Console.SetOut(sw);


			bot.Update(data);


			sw.ToString().Should().Contain("RainBot activated!");
			sw.ToString().Should().Contain("raining");
		}


		[Fact]
		public void SunBot_WhenDisabled_DoesNotWrite()
		{
			var cfg = new Config.BotConfig { Enabled = false, TemperatureThreshold = 20, Message = "sunny" };
			var bot = new SunBot(cfg);
			var data = new WeatherData { Location = "x", Temperature = 40, Humidity = 0 };


			var sw = new StringWriter();
			Console.SetOut(sw);


			bot.Update(data);


			sw.ToString().Should().BeEmpty();
		}

		[Fact]
		public void SnowBot_WhenTemperatureBelowThreshold_WritesToConsole()
		{
			var cfg = new BotConfig { Enabled = true, TemperatureThreshold = 0, Message = "snowy" };
			var bot = new SnowBot(cfg);
			var data = new WeatherData { Location = "X", Temperature = -5, Humidity = 50 };

			var sw = new StringWriter();
			Console.SetOut(sw);

			bot.Update(data);

			sw.ToString().Should().Contain("SnowBot activated!");
			sw.ToString().Should().Contain("snowy");
		}

		[Fact]
		public void SnowBot_WhenTemperatureAboveThreshold_DoesNotWrite()
		{
			var cfg = new BotConfig { Enabled = true, TemperatureThreshold = 0, Message = "snowy" };
			var bot = new SnowBot(cfg);
			var data = new WeatherData { Location = "X", Temperature = 5, Humidity = 50 };

			var sw = new StringWriter();
			Console.SetOut(sw);

			bot.Update(data);

			sw.ToString().Should().BeEmpty();
		}

		[Fact]
		public void SnowBot_WhenDisabled_DoesNotWrite()
		{
			var cfg = new BotConfig { Enabled = false, TemperatureThreshold = 0, Message = "snowy" };
			var bot = new SnowBot(cfg);
			var data = new WeatherData { Location = "X", Temperature = -10, Humidity = 50 };

			var sw = new StringWriter();
			Console.SetOut(sw);

			bot.Update(data);

			sw.ToString().Should().BeEmpty();
		}
	}
}