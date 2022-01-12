using System;
using System.Threading.Tasks;
using MIW_RecommendationsService.Dal.Models;
using MIW_RecommendationsService.Dal.Neo4j.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MIW_RecommendationsService.Messaging
{
    public class ConsumerService : IConsumerService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        private readonly IProductDao _productDao;
        
        public ConsumerService(IRabbitMqService rabbitMqService, IProductDao productDao)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare("CreateProduct", ExchangeType.Fanout, durable: true, autoDelete: false);
            _model.QueueBind(_queueName, "CreateProduct", string.Empty);
            _productDao = productDao;
        }
        
        private readonly string _queueName = "CreateProduct";
        
        public async Task ReadMessages()
        {
            ProductMessage message = new ProductMessage();
            
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var text = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine(text);
                await Task.CompletedTask;
                _model.BasicAck(ea.DeliveryTag, false);
                
                RunMessage(JsonConvert.DeserializeObject<ProductMessage>(text));
            };
            
            _model.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }

        private void RunMessage(ProductMessage message)
        {
            switch (message.MessageType)
            {
                case MessageType.Create:
                    _productDao.Create(message.Product);
                    break;
                default:

                    break;
            }
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}