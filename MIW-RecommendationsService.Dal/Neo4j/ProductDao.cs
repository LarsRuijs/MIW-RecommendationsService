using System.Collections.Generic;
using System.Threading.Tasks;
using MIW_RecommendationsService.Dal.Models;
using MIW_RecommendationsService.Dal.Neo4j.Interfaces;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace MIW_RecommendationsService.Dal.Neo4j
{
    public class ProductDao : IProductDao
    {
        private readonly IDriver _driver;
        
        public ProductDao()
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "test123"));
        }

        // public async Task<Product> GetSingle(long id)
        // {
        //     string query = "CREATE"
        // }

        public async Task<List<Product>> GetAll()
        {
            string query = "MATCH (n:Product) RETURN n";
            var session = _driver.AsyncSession();

            var products = new List<Product>();
            var response = await session.ReadTransactionAsync(
                tx => tx.RunAsync(query));

            foreach (var record in response.ToListAsync().Result)
            {
                var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);
                products.Add(JsonConvert.DeserializeObject<Product>(nodeProps));
            }

            return products;
        }
    }
}