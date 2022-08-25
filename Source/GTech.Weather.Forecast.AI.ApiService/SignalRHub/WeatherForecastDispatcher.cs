using GTech.Weather.Forecast.AI.Domain.MLNET;
using Microsoft.AspNetCore.SignalR;

namespace GTech.Weather.Forecast.AI.ApiService.SignalRHub
{
    public class WeatherForecastDispatcher : IWeatherForecastDispatcher
    {
        private readonly IHubContext<WeatherForecastHub> _hubContext;
        public WeatherForecastDispatcher(IHubContext<WeatherForecastHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task ChangeWeatherForecast(PredictedDailyTemperature predict)
        {
            await this._hubContext.Clients.All.SendAsync("ChangePredict", predict);
        }
    }
}
