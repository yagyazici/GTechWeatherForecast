using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GTech.Weather.Forecast.AI.Integration
{
    public class AccuWeatherService
    {
        public string ApiKey = GetApiKey.Update();
        public string BaseUrl = "http://dataservice.accuweather.com/";
        private readonly HttpClient _Client = new();

        public async Task<string> GetCityKeyAsync(string cityName)
        {
            string FullUrl = $"{BaseUrl}locations/v1/cities/search?apikey={ApiKey}&q={cityName}";
            var response = await _Client.GetStringAsync(FullUrl);
            //var city = JsonConvert.DeserializeObject<JObject>(response);
            //city.ToObject<string>()
            var city = JsonConvert.DeserializeObject<List<JToken>>(response).FirstOrDefault();
            string key = city["key"].ToString();
            return key;
        }
    }
}