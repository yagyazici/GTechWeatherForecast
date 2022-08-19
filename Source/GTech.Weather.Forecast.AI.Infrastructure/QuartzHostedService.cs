using Quartz.Impl;
using Quartz.Logging;
using Quartz;

namespace GTech.Weather.Forecast.AI.Infrastructure
{
    public class QuartzHostedService
    {
        private static async Task Main(string[] args)
        {
            // LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<HelloJob>()
                .UsingJobData("city_name","istanbul")
                .WithIdentity("job1", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }
        private class ConsoleLogProvider : ILogProvider
        {
            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    return true;
                };
            }
            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }
            public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
            {
                throw new NotImplementedException();
            }
        }
    }
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string city_name = dataMap.GetString("city_name");

            await Console.Out.WriteLineAsync(city_name);
        }
    }
}
