using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using MIW_RecommendationsService.Core.Services;
using MIW_RecommendationsService.Core.Services.Interfaces;
using MIW_RecommendationsService.Dal.Models;
using MIW_RecommendationsServiceService.Api.Mappers;

namespace MIW_RecommendationsService.Api.Controllers
{
    public class RecommendationsController : RecommendationsService.RecommendationsServiceBase
    {
        private readonly ILogger<RecommendationsController> _logger;
        private readonly IRecommendationsService _recommendationsService;
        
        public RecommendationsController(ILogger<RecommendationsController> logger,
            IRecommendationsService recommendationsService)
        {
            _logger = logger;
            _recommendationsService = recommendationsService;
        }

        public override async Task GetRecommendations(GetRecommendationsRequest request,
            IServerStreamWriter<RecommendationsProductMessage> responseStream,
            ServerCallContext context)
        {
            _logger.LogInformation("Get Recommendations invoked");
            try
            {
                var responses = _recommendationsService
                    .GetRecommendations(ProductMapper.GetRecommendationsRequestToProductIdList(request));
                foreach (var response in responses.Result)
                {
                    await responseStream.WriteAsync(ProductMapper.ProductToProductResponse(response));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("{E}", e);
                throw new RpcException(new Status(StatusCode.Internal, e.Message));
            }
        }
        
        // public override async Task GetRecommendationsByBasketId(BasketIdMessage basketIdMessage,
        //     IServerStreamWriter<RecommendationsProductMessage> responseStream,
        //     ServerCallContext context)
        // {
        //     _logger.LogInformation("Get Recommendations invoked");
        //     try
        //     {
        //         var responses = _recommendationsService
        //             .GetRecommendations(ProductMapper.GetRecommendationsRequestToProductList(request));
        //         foreach (var response in responses.Result)
        //         {
        //             await responseStream.WriteAsync(ProductMapper.ProductToProductResponse(response));
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.LogError("{E}", e);
        //         throw new RpcException(new Status(StatusCode.Internal, e.Message));
        //     }
        // }
    }
}