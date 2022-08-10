using GTech.Weather.Forecast.AI.Infrastructure;

namespace GTech.Weather.Forecast.AI.Integration
{
    public class WeatherForecastMongoDBService
    {
        public async Task InsertDailyForecastCollection(string cityName)
        {

            var service = new DailyForecastsService();
            var connection = new WeatherForecastMongoDB();
            var collection = connection.WeatherForecastCollection();
            var dailyForecastBSON = service.GetDailyForecastsAsync(cityName).Result;
            await collection.InsertManyAsync(dailyForecastBSON);
        }

        public async Task InsertHourlyForecastCollection(string cityName)
        {
            var service = new HourlyForecastService();
            var connection = new WeatherForecastMongoDB();
            var collection = connection.HourlyWeatherForecastCollection();
            var hourlyForecastBSON = service.GetHourlyForecastsAsync(cityName).Result;
            await collection.InsertManyAsync(hourlyForecastBSON);
        }
    }
}
