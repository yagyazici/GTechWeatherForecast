using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GTech.Weather.Forecast.AI.Domain.WeatherForecast
{
    public class Root
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<List> List { get; set; }
        public City City { get; set; }
    }
    public class City
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
    public class List
    {
        public Main Main { get; set; }
    }
    public class Main
    {
        public double temp_max { get; set; }
    }
}
