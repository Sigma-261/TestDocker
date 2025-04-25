namespace Client.RabbitMq;

public interface ISenderService
{
    Task SendMessage(string message);
}
