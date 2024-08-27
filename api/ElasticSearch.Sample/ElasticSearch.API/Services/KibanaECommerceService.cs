using System.Net;
using ElasticSearch.API.DTOs;
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
         var result = await _kibanaECommerceRepository.GetCustomerByFirstNameTerm(customerFirstName);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), new List<string>(), HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetCustomerByFirstNames(List<string> customerFirstNameList)
      {
         var result = await _kibanaECommerceRepository.GetCustomerByFirstNamesTerms(customerFirstNameList);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), new List<string>(), HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetCustomerByFullName(string customerFullNamePrefix)
      {
         var result = await _kibanaECommerceRepository.GetCustomerByFullNamePrefix(customerFullNamePrefix);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), new List<string>(), HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetProductsByTotalPriceRange(double minPrice, double maxPrice)
      {
         var result = await _kibanaECommerceRepository.GetProductsByTexfulTotalPriceRange(minPrice, maxPrice);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), new List<string>(), HttpStatusCode.OK);
      }

      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetAll()
      {
         var result = await _kibanaECommerceRepository.GetAll();
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), new List<string>(), HttpStatusCode.OK);
      }
   }
}