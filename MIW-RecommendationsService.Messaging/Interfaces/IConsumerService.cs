using System.Threading.Tasks;

namespace MIW_RecommendationsService.Messaging
{
    public interface IConsumerService
    {
        Task ReadMessages();
    }
}