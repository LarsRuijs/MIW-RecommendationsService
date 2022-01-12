using MIW_RecommendationsService.Dal.Models;
using Newtonsoft.Json;

namespace MIW_RecommendationsService.Messaging
{
    public class ProductMessage
    {
        public Product Product { get; set; }
        public MessageType MessageType { get; set; }

        public string GetJsonStr()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}