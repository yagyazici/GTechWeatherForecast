using Microsoft.AspNetCore.Mvc;
using GTech.Weather.Forecast.AI.Integration;
using GTech.Weather.Forecast.AI.Domain.MLNET;
using GTech.Weather.Forecast.AI.ApiService.RabbitMQ;

namespace GTech.Weather.Forecast.AI.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyPredictionController : Controller
    {
        private readonly ILogger<DailyPredictionController> _logger;
        private readonly IRabbitMqService _rabbitMqService;
        public static Dictionary<string, List<PredictedDailyTemperature>> predictDB = new();

        public DailyPredictionController(ILogger<DailyPredictionController> logger, IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
            _logger = logger;
        }

        [HttpGet("{connectionID}")]
        public async Task<List<PredictedDailyTemperature>> Get(string connectionID)
        {
            TimeSeriesService service = new();

            var result = await service.GetMLAsync();
            predictDB.Add(connectionID, result);

            _rabbitMqService.SendNameToQueue(result);

            return result;
        }
    }
}
