using Microsoft.ML.Data;

namespace GTech.Weather.Forecast.AI.Domain
{
    public class MLDailyData
    {
        [LoadColumn(0)]
        public float Value { get; set; }

        [LoadColumn(1)]
        public DateTime Date { get; set; }

    }
}
