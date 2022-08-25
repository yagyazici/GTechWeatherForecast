using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using GTech.Weather.Forecast.AI.Domain.MLNET;

namespace Net5RabbitMqConsumerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { 
                HostName = "localhost",
                UserName = "guest", 
                Password = "123456" 
            };
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                List<PredictedDailyTemperature> deneme = JsonConvert.DeserializeObject<List<PredictedDailyTemperature>>(message);
                Console.WriteLine(deneme.FirstOrDefault().cityName.ToString());

                //Console.WriteLine($"{deneme}");
            };
            channel.BasicConsume(
                queue: "PredictQueue",
                autoAck: true,
                consumer: consumer
            );
            Console.ReadLine();
        }
    }
}