using System.Collections.Generic;
using System.Threading.Tasks;
using MIW_RecommendationsService.Dal.Models;

namespace MIW_RecommendationsService.Core.Services.Interfaces
{
    public interface IRecommendationsService
    {
        Task<List<Product>> GetAll();
    }
}