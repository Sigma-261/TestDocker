using RabbitMQ.Client;
using System.Text;

namespace Client.RabbitMq;

public class SenderService : ISenderService
{
    private IConnection _connection;
    private IChannel _channel;

    public SenderService()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };
        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
    }

    public async Task SendMessage(string message)
    {
        await _channel.QueueDeclareAsync(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: "MyQueue", body: body);
    }
}
