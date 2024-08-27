using System.Net;
using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Repositories;

namespace ElasticSearch.API.Services
{
   public class ProductService
   {
      private readonly ILogger<ProductService> _logger;
      private readonly ProductRepository _productRepository;
      public ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
      {
         _productRepository = productRepository;
         _logger = logger;
      }
      public async Task<ResponseDto<ProductDto>?> SaveAsync(ProductCreateDto request)
      {
         var response = await _productRepository.SaveAsync(request.CreateProduct());
         if (response == null) return ResponseDto<ProductDto>.Fail(new List<string> { "An error occurred while saving the product" }, HttpStatusCode.InternalServerError);
         return ResponseDto<ProductDto>.Success(response.CreateDto(), new List<string>(), HttpStatusCode.Created);
      }
      public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
      {
         var response = await _productRepository.GetAllAsync();
         if (response.Count == 0) return ResponseDto<List<ProductDto>>.Fail(new List<string> { "No products found" }, HttpStatusCode.NotFound);
         return ResponseDto<List<ProductDto>>.Success(response.Select(p => p!.CreateDto()).ToList(), new List<string>(), HttpStatusCode.OK);
      }
      public async Task<ResponseDto<ProductDto>?> GetByIdAsync(string id)
      {
         var response = await _productRepository.GetByIdAsync(id);
         if (response == null) return ResponseDto<ProductDto>.Fail(new List<string> { "Product not found" }, HttpStatusCode.NotFound);
         if (!response.IsSuccess())
         {
            _logger.LogError("An error occurred while getting the product: {Error}", response.ElasticsearchServerError!.Error.Reason);
            return ResponseDto<ProductDto>.Fail(new List<string> { "An error occurred while getting the product" }, HttpStatusCode.InternalServerError);
         }
         return ResponseDto<ProductDto>.Success(response.Source!.CreateDto(), new List<string>(), HttpStatusCode.OK);
      }
      public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto request)
      {
         var response = await _productRepository.UpdateAsync(request);
         if (!response.IsSuccess() && response.Result == Elastic.Clients.Elasticsearch.Result.NotFound) return ResponseDto<bool>.Fail(new List<string> { "Product not found" }, HttpStatusCode.NotFound);
         if (!response.IsSuccess())
         {
            _logger.LogError("An error occurred while updating the product: {Error}", response.ElasticsearchServerError!.Error.Reason);
            return ResponseDto<bool>.Fail(new List<string> { "An error occurred while updating the product" }, HttpStatusCode.InternalServerError);
         }
         return ResponseDto<bool>.Success(response.IsSuccess(), new List<string>(), HttpStatusCode.OK);
      }

      public async Task<ResponseDto<bool>> DeleteAsync(string id)
      {
         var response = await _productRepository.DeleteAsync(id);
         if (!response.IsSuccess() && response.Result == Elastic.Clients.Elasticsearch.Result.NotFound) return ResponseDto<bool>.Fail(new List<string> { "Product not found" }, HttpStatusCode.NotFound);
         if (!response.IsSuccess())
         {
            _logger.LogError("An error occurred while deleting the product: {Error}", response.ElasticsearchServerError!.Error.Reason);
            return ResponseDto<bool>.Fail(new List<string> { "An error occurred while deleting the product" }, HttpStatusCode.InternalServerError);
         }
         return ResponseDto<bool>.Success(response.IsSuccess(), new List<string>(), HttpStatusCode.OK);
      }
      public async Task<ResponseDto<bool>> DeleteAllAsync()
      {
         var response = await _productRepository.DeleteAllAsync();
         if (response.Total == 0) return ResponseDto<bool>.Fail(new List<string> { "No products found" }, HttpStatusCode.NotFound);
         if (!response.IsSuccess())
         {
            _logger.LogError("An error occurred while deleting all products: {Error}", response.ElasticsearchServerError!.Error.Reason);
            return ResponseDto<bool>.Fail(new List<string> { "An error occurred while deleting all products" }, HttpStatusCode.InternalServerError);
         }
         return ResponseDto<bool>.Success(response.IsSuccess(), new List<string>(), HttpStatusCode.OK);
      }
      public async Task<ResponseDto<bool>> IndexExistsAsync()
      {
         var response = await _productRepository.IndexExistsAsync();
         return ResponseDto<bool>.Success(response, new List<string>(), HttpStatusCode.OK);
      }
   }
}
