using System.Collections.Generic;
using System.Threading.Tasks;
using MIW_RecommendationsService.Dal.Models;

namespace MIW_RecommendationsService.Dal.Neo4j.Interfaces
{
    public interface IProductDao
    {
        Task<List<Product>> GetAll();

        Task<Product> Create(Product product);

        Task<List<Product>> GetRecommendations(List<long> productIds);
    }
}