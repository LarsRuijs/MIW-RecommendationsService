using System;
using MIW_RecommendationsService.Dal.Models;
using Neo4j.Driver;

namespace MIW_RecommendationsService.Dal.Neo4j
{
    public static class Neo4jMapper
    {
        public static Product INodeToProduct(INode node)
        {
            return new ()
            {
                Id = node.Properties["id"].As<long>(),
                Name = node.Properties["name"].As<string>(),
                Company = node.Properties["company"].As<string>(),
                Price = node.Properties["price"].As<decimal>(),
                Discount = node.Properties["discount"].As<decimal>(),
                ImgLink = node.Properties["imgLink"].As<string>()
            };
        }
    }
}