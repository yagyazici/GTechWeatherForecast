using Quartz.Impl;
using GTech.Weather.Forecast.AI.Integration;
using Quartz;
using GTech.Weather.Forecast.AI.ApiService.SignalRHub;
using GTech.Weather.Forecast.AI.ApiService.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddTransient<IRabbitMqService, RabbitMqService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

//builder.Services.AddMassTransit(x =>
//{
//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host("localhost", "/", h => {
//            h.Username("guest");
//            h.Password("123456");
//        });
//        cfg.ConfigureEndpoints(context);
//    });
//});

//signalR
builder.Services.AddSignalR()
                  .AddJsonProtocol(options => {
                      options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                  });

builder.Services.AddSingleton<IWeatherForecastDispatcher, WeatherForecastDispatcher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app cors
app.UseCors("MyPolicy");
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();

//quartz
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
        .WithIntervalInMinutes(1)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(job, trigger);

//signalR
app.MapHub<WeatherForecastHub>("/weatherForecastHub");
app.MapControllers();

app.Run();

