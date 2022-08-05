using MongoDB.Driver;
namespace GTech.Weather.Forecast.AI.Infrastructure
{
    public class WeatherForecastMongoDB
    {
        public void TestDatabaseConnection()
        {
            MongoClient client = new MongoClient("mongodb://localhost:7296/?serverSelectionTimeoutMS=5000&connectTimeoutMS=10000&3t.uriVersion=3&3t.connection.name=WeatherForecastMongoDB&3t.alwaysShowAuthDB=true&3t.alwaysShowDBFromUserRole=true");
            var dbList = client.ListDatabases().ToList();
            Console.WriteLine(dbList.Count);
        }
    }
}
