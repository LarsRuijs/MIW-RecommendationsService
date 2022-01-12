using System;
using System.Collections.Generic;
using MIW_RecommendationsService.Api;
using MIW_RecommendationsService.Dal.Models;

namespace MIW_RecommendationsServiceService.Api.Mappers
{
    public static class ProductMapper
    {
        public static RecommendationsProductMessage ProductToProductResponse(Product product)
        {
            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Company = product.Company,
                Price = Decimal.ToInt32(product.Price),
                Discount = Decimal.ToInt32(product.Discount),
                ImgLink = product.ImgLink
            };
        }

        public static RecommendationMessage RecommendationToRecommendationMessage(Recommendation recommendation)
        {
            return new()
            {
                Product = ProductToProductResponse(recommendation.Product),
                Priority = recommendation.Priority
            };
        }

        // public static List<Product> GetRecommendationsRequestToProductList(GetRecommendationsRequest request)
        // {
        //     var products = new List<Product>();
        //     foreach (var product in request.Products)
        //     {
        //         products.Add(new Product()
        //         {
        //             Id = product.Id,
        //             Name = product.Name,
        //             Company = product.Company,
        //             Price = product.Price,
        //             Discount = product.Discount,
        //             ImgLink = product.ImgLink
        //         });
        //     }
        //
        //     return products;
        // }

        public static List<long> GetRecommendationsRequestToProductIdList(GetRecommendationsRequest request)
        {
            var productIdList = new List<long>();
            foreach (var productId in request.ProductIds)
            {
                productIdList.Add(productId);
            }

            return productIdList;
        }

        // public static Product CreateProductRequestToProduct(CreateProductRequest createProductRequest)
        // {
        //     return new()
        //     {
        //         Name = createProductRequest.Name,
        //         Company = createProductRequest.Company,
        //         Price = createProductRequest.Price,
        //         Discount = createProductRequest.Discount,
        //         ImgLink = createProductRequest.ImgLink
        //     };
        // }
    }
}