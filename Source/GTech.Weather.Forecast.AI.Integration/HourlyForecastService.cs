using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
using Newtonsoft.Json;

namespace GTech.Weather.Forecast.AI.Integration
{
    public class HourlyForecastService
    {
        public string ApiKey = GetApiKey.Update();
        public string BaseUrl = "http://dataservice.accuweather.com/";
        private readonly HttpClient _Client = new();

        public async Task<List<Hourly>> GetHourlyForecastsAsync(string cityName)
        {
            var service = new AccuWeatherService();
            var result = await service.GetCityKeyAsync(cityName);
            string FullUrl = $"{BaseUrl}currentconditions/v1/{result}/historical/24?apikey={ApiKey}";
            string response = await _Client.GetStringAsync(FullUrl);
            List<Hourly> hourlyForecasts = JsonConvert.DeserializeObject<List<Hourly>>(response);
            return hourlyForecasts;
        }
    }
}