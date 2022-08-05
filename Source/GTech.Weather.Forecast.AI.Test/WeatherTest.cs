using GTech.Weather.Forecast.AI.Integration;
using Newtonsoft.Json;

namespace GTech.Weather.Forecast.AI.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [TestCase ("Istanbul")]
        public void City_GetCityName_Success(string cityName)
        {
            AccuWeatherService service = new();
            var result = service.GetCityKeyAsync(cityName).Result;
            Console.WriteLine(result);
        }
        [TestCase("Mersin")]
        public async Task DailyForecast_GetDailyForecastsAsync_Success(string cityName)
        {
            DailyForecastsService service = new();
            var result = await service.GetDailyForecastsAsync(cityName);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}