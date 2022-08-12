using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
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
        public PredictedDailyTemperature GetMLAsync()
        {
            var collection = connection.WeatherForecastCollection();
            var documents = collection.Find(FilterDefinition<DailyForecast>.Empty).ToEnumerable();
            List<MLDailyData> listMlDailyForecast = documents.Select(i => new MLDailyData{
                Value = (float)(i.Temperature.Maximum.Value),
                Date = (DateTime)i.Date
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
            var forecasts = forecastingEngine.Predict();
            var predict = new PredictedDailyTemperature
            {
                Date = listMlDailyForecast.FirstOrDefault().Date,
                Temperatures = forecasts.Forecast.ToList()
            };
            return predict;
        }
    }
} 