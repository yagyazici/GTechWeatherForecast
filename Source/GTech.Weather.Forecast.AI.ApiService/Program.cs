using Quartz.Impl;
using Quartz.Logging;
using GTech.Weather.Forecast.AI.Integration;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
//app cors
app.UseCors("corsapp");
app.UseHttpsRedirection();
app.UseAuthorization();

StdSchedulerFactory factory = new StdSchedulerFactory();
IScheduler scheduler = await factory.GetScheduler();

await scheduler.Start();

IJobDetail job = JobBuilder.Create<CityQuartzJobService.QuartzJob>()
    .UsingJobData("city_name","istanbul")
    .WithIdentity("job1", "group1")
    .Build();

ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInMinutes(10)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(job, trigger);

app.MapControllers();

app.Run();

