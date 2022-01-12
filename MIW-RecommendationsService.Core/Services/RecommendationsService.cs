using System.Collections.Generic;
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
        
        public Task<List<Product>> GetAll()
        {
            return _productDao.GetAll();
        }

        public Task<List<Product>> GetRecommendations(List<long> productIds)
        {
            return _productDao.GetRecommendations(productIds);
        }

        public Task<List<Product>> GetRecommendations(long basketId)
        {
            throw new System.NotImplementedException();
        }
    }
}