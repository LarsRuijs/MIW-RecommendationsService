using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MIW_RecommendationsService.Messaging
{
    public class ConsumerHostedService : BackgroundService
    {
        private readonly IConsumerService _consumerService;

        public ConsumerHostedService(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumerService.ReadMessages();
        }
    }
}