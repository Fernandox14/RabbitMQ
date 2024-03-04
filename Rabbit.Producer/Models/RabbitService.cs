using Newtonsoft.Json;
using Rabbit.Producer.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Rabbit.Producer.Models
{
    public class RabbitService: IRabbitService
    {
        private readonly ConnectionFactory _connectionFactory;
        private string QUEUE_NAME = "top_category";

        public RabbitService()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public void PostMessage(Category message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: true,
                        autoDelete: false,
                        exclusive: false,
                        arguments: null
                    );

                    var stringfield = JsonConvert.SerializeObject(message);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfield);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesMessage
                    );
                }
            }
        }
    }
}
