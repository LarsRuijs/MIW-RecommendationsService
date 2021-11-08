using System;
using MIW_RecommendationsService.Api;
using MIW_RecommendationsService.Dal.Models;
using MIW_RecommendationsService.Api;
using MIW_RecommendationsService.Dal.Models;

namespace MIW_RecommendationsServiceService.Api.Mappers
{
    public class ProductMapper
    {
        public static ProductResponse ProductToProductResponse(Product product)
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