using MongoDB.Driver;
using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
namespace GTech.Weather.Forecast.AI.Infrastructure
{
    public class WeatherForecastMongoDB
    {
        public IMongoCollection<Root> WeatherForecastCollection()
        {
            MongoClient client = new("mongodb://localhost:27017/");
            var database = client.GetDatabase("WeatherForecastDB");
            var collection = database.GetCollection<Root>("WeatherForecastCollection");
            return collection;
        }
    }
}
