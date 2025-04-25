using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Server.RabbitMq;

public class ConsumerService : BackgroundService
{
    private IConnection _connection;
    private IChannel _channel;

    public ConsumerService()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "rabbit"
        };
        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
        _channel.QueueDeclareAsync(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

            // Каким-то образом обрабатываем полученное сообщение
            Console.WriteLine(content);

            _channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        _channel.BasicConsumeAsync("MyQueue", false, consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.CloseAsync();
        _connection.CloseAsync();
        base.Dispose();
    }
}
