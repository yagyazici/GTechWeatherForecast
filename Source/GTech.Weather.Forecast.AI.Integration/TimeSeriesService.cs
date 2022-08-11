using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
using MongoDB.Driver;
using Microsoft.ML;
using GTech.Weather.Forecast.AI.Infrastructure;
using GTech.Weather.Forecast.AI.Domain;
using Microsoft.ML.Transforms.TimeSeries;
using DnsClient;
using SharpCompress.Readers;
using System.Diagnostics;
using System.Drawing;
using System.Data;

namespace GTech.Weather.Forecast.AI.Integration
{
    public class TimeSeriesService
    {
        MLContext context = new();
        WeatherForecastMongoDB connection = new();
        public async Task GetMLAsync()
        {
            try
            {
                var collection = connection.WeatherForecastCollection();
                var documents = collection.Find(FilterDefinition<DailyForecast>.Empty).ToEnumerable();
                
                var listMlDailyForecast = new List<MLDailyData>();

                foreach (var document in documents)
                {
                    listMlDailyForecast.Add(new MLDailyData
                    {
                        Value = (float)document.Temperature.Maximum.Value,
                        Date = (DateTime)document.Date
                    });
                }

                var data = context.Data.LoadFromEnumerable(listMlDailyForecast);
                var inputColumnName = nameof(MLDailyData.Value);
                var outputColumnName = nameof(MLDailyForecast.Forecast);

                var pipeline = context.Forecasting.ForecastBySsa(
                    outputColumnName,
                    inputColumnName,
                    windowSize: 2,
                    seriesLength: 4,
                    trainSize: listMlDailyForecast.Count,
                    horizon: 10
                    );

                var model = pipeline.Fit(data);
                var forecastingEngine = model.CreateTimeSeriesEngine<MLDailyData, MLDailyForecast>(context);
                var forecasts = forecastingEngine.Predict();

                foreach(var forecast in forecasts.Forecast)
                {
                    Console.WriteLine(forecast);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}