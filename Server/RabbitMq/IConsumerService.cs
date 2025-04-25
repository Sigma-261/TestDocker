namespace Server.RabbitMq;

public interface IConsumerService
{
    Task SendMessage(string message);
}
