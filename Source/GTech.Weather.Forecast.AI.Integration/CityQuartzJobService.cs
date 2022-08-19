using Quartz;
namespace GTech.Weather.Forecast.AI.Integration
{
    public class CityQuartzJobService
    {
        public class QuartzJob : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                TimeSeriesService service = new TimeSeriesService();
                await service.GetMLAsync();
            }
        }
    }
}
