﻿using Quartz;
namespace GTech.Weather.Forecast.AI.Integration
{
    public class CityQuartzJobService
    {
        public class QuartzJob : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                WeatherForecastMongoDBService mongoService = new();
                await mongoService.ClearDailyForecastCollection();
                await mongoService.InsertDailyForecastCollection();
            }
        }
    }
}
