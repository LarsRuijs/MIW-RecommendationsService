using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MIW_RecommendationsService.Core.Services.Interfaces;
using MIW_RecommendationsService.Dal.Models;
using MIW_RecommendationsService.Dal.Neo4j.Interfaces;

namespace MIW_RecommendationsService.Core.Services
{
    public class RecommendationsService : IRecommendationsService
    {
        private readonly IProductDao _productDao;

        public RecommendationsService(IProductDao productDao)
        {
            _productDao = productDao;
        }
        
        public async Task<List<Product>> GetAll()
        {
            return await _productDao.GetAll();
        }

        public async Task<List<Recommendation>> GetRecommendations(List<long> productIds)
        {
            List<Product> products = await _productDao.GetRecommendations(productIds);
            
            var recommendations = new List<Recommendation>();
            var skipQueue = new List<long>();
            foreach (var product in products)
            {
                // Continue if duplicate product is found or if product is already being ordered by the customer.
                if (skipQueue.Contains(product.Id) || productIds.Contains(product.Id))
                {
                    continue;
                }
                
                skipQueue.Add(product.Id);
                
                recommendations.Add(new Recommendation()
                    {
                        Product = product,
                        // How often was the same product recommended?
                        Priority = products.Count(x => x.Id == product.Id)
                    });
                
            }

            return recommendations
                .OrderByDescending(x => x.Priority)
                .Take(5)
                .ToList();
        }

        public Task<List<Product>> GetRecommendations(long basketId)
        {
            throw new System.NotImplementedException();
        }
    }
}