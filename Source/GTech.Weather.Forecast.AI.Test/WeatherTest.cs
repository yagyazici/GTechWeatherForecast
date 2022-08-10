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
        [TestCase("Istanbul")]
        public void City_GetCityName_Success(string cityName)
        {
            AccuWeatherService service = new();
            var result = service.GetCityKeyAsync(cityName).Result;
            Console.WriteLine(result);
        }

        [TestCase("Manisa")]
        public async Task DailyForecast_GetDailyForecastsAsync_Success(string cityName)
        {
            DailyForecastsService service = new();
            var result = await service.GetDailyForecastsAsync(cityName);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [TestCase("Antalya")]
        public async Task HourlyForecast_GetHourlyForecastsAsync_Success(string cityName)
        {
            HourlyForecastService service = new();
            var result = await service.GetHourlyForecastsAsync(cityName);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [TestCase("Antalya")]
        public async Task InsertDailyForecastCollection_Success(string cityName)
        {
            WeatherForecastMongoDBService service = new();
            await service.InsertDailyForecastCollection(cityName);
            Console.WriteLine("Done!");

        }
        [TestCase ("Antalya")]
        public async Task InsertHourlyForecastCollection_Success(string cityName)
        {
            WeatherForecastMongoDBService service = new();
            await service.InsertHourlyForecastCollection(cityName);
            Console.WriteLine("Done!");
        }
        
        [TestCase]
        public async Task Deneme()
        {
            TimeSeriesService service = new();
            await service.GetMLAsync12();
            Console.WriteLine("done");
        }
    }
}