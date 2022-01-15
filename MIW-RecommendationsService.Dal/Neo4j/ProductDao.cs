using System;
using System.Collections.Generic;
using System.Linq;
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
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "neo4j123"));
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

        public async Task<Product> Create(Product product)
        {
            string queryVars = String.Format("id:'{0}',name:'{1}',company:'{2}',price:{3},discount:{4},imgLink:'{5}'",
                product.Id, product.Name, product.Company, product.Price, product.Discount, product.ImgLink);
            string query = "CREATE (pr1:Product{" + queryVars + "})  RETURN pr1";
            var session = _driver.AsyncSession();
            
            try
            {
                return await session.ReadTransactionAsync(
                    async tx =>
                    {
                        var newProduct = new Product();
                        
                        var result = await tx.RunAsync(query);

                        while (await result.FetchAsync())
                        {
                            INode node = result.Current[0] as INode;
                            newProduct = Neo4jMapper.INodeToProduct(node);
                        }
                        
                        return newProduct;
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<Product>> GetRecommendations(List<long> productIds)
        {
            var recommendations = new List<Product>();

            try
            {
                foreach (var productId in productIds)
                {
                    string queryVars = String.Format("id:{0}", productId);
                    string query = "MATCH (pr:Product {" + queryVars +
                                   "})<-[co1:CONTAINS]-(ba1:Basket)<-[cr1:CREATED]-(pe1:Person)" +
                                   "MATCH (pe1)-[cr2:CREATED]->(ba2:Basket)-[co2:CONTAINS]->(re:Product)" +
                                   "RETURN re";
                    var session = _driver.AsyncSession();
                    
                    await session.ReadTransactionAsync(
                        async tx =>
                        {
                            IResultCursor result = await tx.RunAsync(query);
                            List<IRecord> response = await result.ToListAsync();

                            foreach (var record in response)
                            {
                                var node = record.Values["re"] as INode;
                                recommendations.Add(Neo4jMapper.INodeToProduct(node));
                            }
                            
                            Console.WriteLine(recommendations);
                        });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return recommendations;
        }
    }
}