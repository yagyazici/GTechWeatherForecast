using GTech.Weather.Forecast.AI.Infrastructure;
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

        [TestCase("Manisa")]
        public async Task DailyForecast_GetDailyForecastsAsync_Success(string cityName)
        {
            DailyForecastsService service = new();
            var result = await service.GetDailyForecastsAsync(cityName);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [TestCase]
        public void WeatherForecastCollection_Success()
        {
            WeatherForecastMongoDB service = new();
            var result = service.WeatherForecastCollection();
            Console.WriteLine(result);
        }

        [TestCase]
        public async Task InsertDailyForecastCollection_Success()
        {
            WeatherForecastMongoDBService service = new();
            await service.InsertDailyForecastCollection();
            Console.WriteLine("Done!");
        }

        [TestCase]
        public async Task RemoveDailyForecastCollection_Success()
        {
            WeatherForecastMongoDBService service = new();
            await service.ClearDailyForecastCollection();
            Console.WriteLine("Done!");
        }

        [TestCase]
        public async Task TimeSeriesService_Success()
        {
            TimeSeriesService service = new();
            await service.GetMLAsync();
        }
    }
}