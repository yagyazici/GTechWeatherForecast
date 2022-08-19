using Microsoft.AspNetCore.Mvc;
using GTech.Weather.Forecast.AI.Integration;
using GTech.Weather.Forecast.AI.Domain.MLNET;

namespace GTech.Weather.Forecast.AI.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyPredictionController : Controller
    {
        private readonly ILogger<DailyPredictionController> _logger;
        public DailyPredictionController(ILogger<DailyPredictionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDailyPrediction")]
        public async Task<List<PredictedDailyTemperature>> Get()
        {
            WeatherForecastMongoDBService mongoService = new();
            await mongoService.ClearDailyForecastCollection();
            await mongoService.InsertDailyForecastCollection();

            TimeSeriesService service = new TimeSeriesService();

            return await service.GetMLAsync();
        }
    }
}
