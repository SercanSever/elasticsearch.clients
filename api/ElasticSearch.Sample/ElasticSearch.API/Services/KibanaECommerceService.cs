using System.Net;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Repositories;

namespace ElasticSearch.API.Services
{
   public class KibanaECommerceService(KibanaECommerceRepository kibanaECommerceRepository, ILogger<KibanaECommerceService> logger)
   {
      private readonly ILogger<KibanaECommerceService> _logger = logger;
      private readonly KibanaECommerceRepository _kibanaECommerceRepository = kibanaECommerceRepository;

      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetCustomerByFirstName(string customerFirstName)
      {
         _logger.LogInformation("GetCustomerByFirstName method called");
         var result = await _kibanaECommerceRepository.GetCustomerByFirstNameTerm(customerFirstName);
         if (result.Count == 0) return ResponseDto<List<KibanaECommerceDto>>.Fail(["No customers found"], HttpStatusCode.NotFound);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetCustomerByFirstNames(List<string> customerFirstNameList)
      {
         _logger.LogInformation("GetCustomerByFirstNames method called");
         var result = await _kibanaECommerceRepository.GetCustomerByFirstNamesTerms(customerFirstNameList);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetCustomerByFullName(string customerFullNamePrefix)
      {
         _logger.LogInformation("GetCustomerByFullName method called");
         var result = await _kibanaECommerceRepository.GetCustomerByFullNamePrefix(customerFullNamePrefix);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetProductsByTotalPriceRange(double minPrice, double maxPrice)
      {
         _logger.LogInformation("GetProductsByTotalPriceRange method called");
         var result = await _kibanaECommerceRepository.GetProductsByTexfulTotalPriceRange(minPrice, maxPrice);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetAll()
      {
         _logger.LogInformation("GetAll method called");
         var result = await _kibanaECommerceRepository.GetAll();
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetAllWithPagination(int page, int pageSize)
      {
         _logger.LogInformation("GetAllWithPagination method called");
         var result = await _kibanaECommerceRepository.GetAllWithPagination(page, pageSize);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetWithWildCard(string seacrh)
      {
         _logger.LogInformation("GetWithWildCard method called");
         var result = await _kibanaECommerceRepository.GetWithWildCard(seacrh);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetByCustomerNameFuzzy(string customerFirstName)
      {
         _logger.LogInformation("GetByCustomerNameFuzzy method called");
         var result = await _kibanaECommerceRepository.GetByCustomerNameFuzzy(customerFirstName);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<List<KibanaECommerceDto>>> GetByCategoryMatch(string categoryName)
      {
         _logger.LogInformation("GetByCategory method called");
         var result = await _kibanaECommerceRepository.GetByCategoryMatch(categoryName);
         return ResponseDto<List<KibanaECommerceDto>>.Success(result.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
   }
}