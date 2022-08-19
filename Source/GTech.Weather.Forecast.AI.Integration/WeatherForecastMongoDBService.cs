using GTech.Weather.Forecast.AI.Infrastructure;
using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
namespace GTech.Weather.Forecast.AI.Integration
{
    public class WeatherForecastMongoDBService
    {
        public async Task ClearDailyForecastCollection()
        {
            var connection = new WeatherForecastMongoDB();
            var collection = connection.WeatherForecastCollection();
            await collection.DeleteManyAsync("{}");
        }
        public async Task InsertDailyForecastCollection()
        {
            List<string> cities = new List<string> {"ankara", "istanbul", "izmir"};
            var service = new DailyForecastsService();

            // List<Root> dailyForecasts = cities.Select(async i => 
            //     await service.GetDailyForecastsAsync(i)
            // );
            List<Root> dailyForecasts = new();
            foreach (var city in cities)
            {
                dailyForecasts.Add(
                    await service.GetDailyForecastsAsync(city)
                );
            }

            var connection = new WeatherForecastMongoDB();
            var collection = connection.WeatherForecastCollection();
            
            await collection.InsertManyAsync(dailyForecasts);
        }
    }
}
