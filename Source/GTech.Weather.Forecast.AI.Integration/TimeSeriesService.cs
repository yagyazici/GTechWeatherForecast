using GTech.Weather.Forecast.AI.Infrastructure;
using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.ML;
using Newtonsoft.Json;

namespace GTech.Weather.Forecast.AI.Integration
{
    public class TimeSeriesService
    {
        MLContext context = new();
        public async Task GetMLAsync12()
        {
            var mongo_client = new MongoClient("mongodb://localhost:7296/?serverSelectionTimeoutMS=5000&connectTimeoutMS=10000&3t.uriVersion=3&3t.connection.name=WeatherForecastMongoDB&3t.alwaysShowAuthDB=true&3t.alwaysShowDBFromUserRole=true");
            var mongo_database = mongo_client.GetDatabase("WeatherForecastDB");
            var collection = mongo_database.GetCollection<DailyForecast>("DailyForecast");
            var documents = collection.Find(FilterDefinition<DailyForecast>.Empty).ToEnumerable();
            Console.WriteLine(documents.FirstOrDefault().GetType());
            IDataView data = context.Data.LoadFromEnumerable(documents);
            Console.WriteLine(data);
        }
    }
}