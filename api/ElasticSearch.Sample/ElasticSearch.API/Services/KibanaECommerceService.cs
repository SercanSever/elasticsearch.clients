using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models.ECommerceModel;
using ElasticSearch.API.Repositories;

namespace ElasticSearch.API.Services
{
   public class KibanaECommerceService
   {
      private readonly ILogger<KibanaECommerceService> _logger;
      private readonly KibanaECommerceRepository _kibanaECommerceRepository;

      public KibanaECommerceService(KibanaECommerceRepository kibanaECommerceRepository, ILogger<KibanaECommerceService> logger)
      {
         _kibanaECommerceRepository = kibanaECommerceRepository;
         _logger = logger;
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetCustomerByFirstName(string customerFirstName)
      {
         var result = await _kibanaECommerceRepository.TermQuesry(customerFirstName);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), new List<string>(), HttpStatusCode.OK);
      }
   }
}