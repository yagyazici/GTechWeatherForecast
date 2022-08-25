using GTech.Weather.Forecast.AI.Domain.MLNET;

namespace GTech.Weather.Forecast.AI.ApiService.RabbitMQ
{
    public interface IRabbitMqService
    {
        void SendNameToQueue(List<PredictedDailyTemperature> predict);
    }
}
