// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");
string QUEUE_NAME = "top_category";

var _connectionFactory = new ConnectionFactory
{
    HostName = "rabbitmq",
    Port = 5672,
    UserName = "guest",
    Password = "guest"
};

using (var connection = _connectionFactory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(
            queue: QUEUE_NAME,
            durable: true,
            autoDelete: false,
            exclusive: false,
            arguments: null
        );

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };

        channel.BasicConsume(queue: QUEUE_NAME, autoAck: true, consumer: consumer);
        Console.ReadLine();
    }
}
