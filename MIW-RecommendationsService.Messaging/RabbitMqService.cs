using RabbitMQ.Client;

namespace MIW_RecommendationsService.Messaging
{
    public class RabbitMqService : IRabbitMqService
    {
        public RabbitMqService()
        {
            
        }
        
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }
    }
}