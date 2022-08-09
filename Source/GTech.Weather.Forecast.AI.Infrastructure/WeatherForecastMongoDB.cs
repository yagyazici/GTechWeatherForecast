using MongoDB.Driver;
using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
namespace GTech.Weather.Forecast.AI.Infrastructure
{
    public class WeatherForecastMongoDB
    {
        public IMongoCollection<DailyForecast> WeatherForecastCollection()
        {
            MongoClient client = new ("mongodb://localhost:7296/?serverSelectionTimeoutMS=5000&connectTimeoutMS=10000&3t.uriVersion=3&3t.connection.name=WeatherForecastMongoDB&3t.alwaysShowAuthDB=true&3t.alwaysShowDBFromUserRole=true");
            var database = client.GetDatabase("WeatherForecastDB");
            var collection = database.GetCollection<DailyForecast>("DailyForecast");
            return collection;
        }
        public IMongoCollection<Hourly> HourlyWeatherForecastCollection()
        {
            MongoClient client = new("mongodb://localhost:7296/?serverSelectionTimeoutMS=5000&connectTimeoutMS=10000&3t.uriVersion=3&3t.connection.name=WeatherForecastMongoDB&3t.alwaysShowAuthDB=true&3t.alwaysShowDBFromUserRole=true");
            var database = client.GetDatabase("WeatherForecastDB");
            var collection = database.GetCollection<Hourly>("HourlyForecast");
            return collection;
        }
    }
}
