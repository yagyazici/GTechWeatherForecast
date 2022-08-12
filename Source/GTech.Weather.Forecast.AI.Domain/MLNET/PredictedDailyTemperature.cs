namespace GTech.Weather.Forecast.AI.Domain.MLNET
{
    public class PredictedDailyTemperature
    {
        public List<float> Temperatures { get; set; }
        public DateTime Date { get; set; }
    }
}
