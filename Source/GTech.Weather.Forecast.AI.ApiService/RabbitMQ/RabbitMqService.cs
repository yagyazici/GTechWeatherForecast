using GTech.Weather.Forecast.AI.Domain.MLNET;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace GTech.Weather.Forecast.AI.ApiService.RabbitMQ
{
    public class RabbitMqService: IRabbitMqService
    {
        public void SendNameToQueue(List<PredictedDailyTemperature> predict)
        {
            var factory = new ConnectionFactory() { 
                HostName = "localhost",
                UserName = "guest",
                Password = "123456" 
            };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "PredictQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var body = Encoding.UTF8.GetBytes(
                        JsonConvert.SerializeObject(predict)
                );

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "PredictQueue",
                    body: body
                );
            }
        }
    }
}
