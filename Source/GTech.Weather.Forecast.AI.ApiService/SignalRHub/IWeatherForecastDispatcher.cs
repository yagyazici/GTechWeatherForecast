using GTech.Weather.Forecast.AI.Domain.MLNET;

namespace GTech.Weather.Forecast.AI.ApiService.SignalRHub
{
    public interface IWeatherForecastDispatcher
    {
        Task ChangeWeatherForecast(PredictedDailyTemperature predict);
    }
}
