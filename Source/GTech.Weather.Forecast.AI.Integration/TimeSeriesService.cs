using MongoDB.Driver;
using Microsoft.ML;
using GTech.Weather.Forecast.AI.Infrastructure;
using Microsoft.ML.Transforms.TimeSeries;
using GTech.Weather.Forecast.AI.Domain.MLNET;

namespace GTech.Weather.Forecast.AI.Integration
{
    public class TimeSeriesService
    {
        MLContext context = new();
        WeatherForecastMongoDB connection = new();
        public async Task<List<PredictedDailyTemperature>> GetMLAsync()
        {
            var collection = connection.WeatherForecastCollection();
            var documents = collection.Find(_ => true).ToList();

            List<PredictedDailyTemperature> predicts = new();

            foreach (var doc in documents)
            {
                List<MLDailyData> listMlDailyForecast = doc.List.Select(i => new MLDailyData
                {
                    Value = (float)(i.Main.temp_max)
                }).ToList();
                var data = context.Data.LoadFromEnumerable(listMlDailyForecast);
                var pipeline = context.Forecasting.ForecastBySsa(
                    nameof(MLDailyForecast.Forecast),
                    nameof(MLDailyData.Value),
                    windowSize: 2,
                    seriesLength: 4,
                    trainSize: listMlDailyForecast.Count,
                    horizon: 10
                );
                var model = pipeline.Fit(data);
                var forecastingEngine = model.CreateTimeSeriesEngine<MLDailyData, MLDailyForecast>(context);
                var forecasts = forecastingEngine.Predict().Forecast.ToList();
                List<Temperatures> temps = forecasts.Select((value, index) => new Temperatures
                {
                    Temperature = value,
                    Date = DateTime.Today.AddDays(5 + index)
                }).ToList();
                var predict = new PredictedDailyTemperature
                {
                    cityName = doc.City.Name,
                    Temperatures = temps
                };
                predicts.Add(predict);
            }
            return predicts;
        }
    }
}