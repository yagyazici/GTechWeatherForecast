using GTech.Weather.Forecast.AI.ApiService.Controllers;
using GTech.Weather.Forecast.AI.Domain.MLNET;
using Microsoft.AspNetCore.SignalR;

namespace GTech.Weather.Forecast.AI.ApiService.SignalRHub
{
    public class WeatherForecastHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync ("GetConnectionId", this.Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            DailyPredictionController.predictDB.Remove(this.Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task ClearProduct(PredictedDailyTemperature predict)
        {
            await Clients.All.SendAsync("ChangeGame", predict);
        }
    }
}
