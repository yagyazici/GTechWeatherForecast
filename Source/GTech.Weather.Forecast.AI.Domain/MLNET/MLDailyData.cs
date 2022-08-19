using Microsoft.ML.Data;

namespace GTech.Weather.Forecast.AI.Domain.MLNET
{
    public class MLDailyData
    {
        [LoadColumn(0)]
        public float Value { get; set; }
    }
}
