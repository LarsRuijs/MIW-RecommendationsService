using RabbitMQ.Client;

namespace MIW_RecommendationsService.Messaging
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }
}