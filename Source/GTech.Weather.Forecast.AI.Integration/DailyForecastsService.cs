using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
using Newtonsoft.Json;

namespace GTech.Weather.Forecast.AI.Integration
{
    public class DailyForecastsService
    {
        public string ApiKey = "UyeghCuMmB3O6xHc14mp08QBNEsjqbPj";
        public string BaseUrl = "http://dataservice.accuweather.com/";
        private readonly HttpClient _Client = new();

        public async Task<DailyForecast> GetDailyForecastsAsync(string cityName)
        {
            var service = new AccuWeatherService();
            var result = await service.GetCityKeyAsync(cityName);
            string FullUrl = $"{BaseUrl}forecasts/v1/daily/1day/{result}?apikey={ApiKey}&language=en-us&metric=true";
            string response = await _Client.GetStringAsync(FullUrl);
            Root city = JsonConvert.DeserializeObject<Root>(response);
            return city.DailyForecasts.FirstOrDefault();
        }
    }
}
