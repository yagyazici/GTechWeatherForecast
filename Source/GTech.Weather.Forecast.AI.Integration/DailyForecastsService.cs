using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
using Newtonsoft.Json;

namespace GTech.Weather.Forecast.AI.Integration
{
    public class DailyForecastsService
    {
        public string ApiKey = GetApiKey.Update();
        public string BaseUrl = "https://api.openweathermap.org/data/2.5/forecast?";
        private readonly HttpClient _Client = new();

        public async Task<Root> GetDailyForecastsAsync(string cityName)
        {
            string FullUrl = $"{BaseUrl}q={cityName}&appid={ApiKey}&lang=tr&units=metric";
            string response = await _Client.GetStringAsync(FullUrl);
            Root root = JsonConvert.DeserializeObject<Root>(response);
            return root;
        }
    }
}
