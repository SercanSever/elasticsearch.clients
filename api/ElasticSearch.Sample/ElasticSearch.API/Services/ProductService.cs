using System.Net;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Repositories;

namespace ElasticSearch.API.Services
{
   public class ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
   {
      private readonly ILogger<ProductService> _logger = logger;
      private readonly ProductRepository _productRepository = productRepository;

      public async Task<ResponseDto<ProductDto>?> SaveAsync(ProductCreateDto request)
      {
         var response = await _productRepository.SaveAsync(request.CreateProduct());
         if (response == null) return ResponseDto<ProductDto>.Fail(["An error occurred while saving the product"], HttpStatusCode.InternalServerError);
         return ResponseDto<ProductDto>.Success(response.CreateDto(), [], HttpStatusCode.Created);
      }
      public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
      {
         var response = await _productRepository.GetAllAsync();
         if (response.Count == 0) return ResponseDto<List<ProductDto>>.Fail(["No products found"], HttpStatusCode.NotFound);
         return ResponseDto<List<ProductDto>>.Success(response.Select(p => p!.CreateDto()).ToList(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<ProductDto>?> GetByIdAsync(string id)
      {
         var response = await _productRepository.GetByIdAsync(id);
         if (response == null) return ResponseDto<ProductDto>.Fail(["Product not found"], HttpStatusCode.NotFound);
         if (!response.IsValidResponse)
         {
            _logger.LogError("An error occurred while getting the product: {Error}", response.TryGetOriginalException(out _));
            return ResponseDto<ProductDto>.Fail(["An error occurred while getting the product"], HttpStatusCode.InternalServerError);
         }
         return ResponseDto<ProductDto>.Success(response.Source!.CreateDto(), [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto request)
      {
         var response = await _productRepository.UpdateAsync(request);
         if (!response.IsValidResponse && response.Result == Elastic.Clients.Elasticsearch.Result.NotFound) return ResponseDto<bool>.Fail(["Product not found"], HttpStatusCode.NotFound);
         if (!response.IsValidResponse)
         {
            _logger.LogError("An error occurred while updating the product: {Error}", response.TryGetOriginalException(out _));
            return ResponseDto<bool>.Fail(["An error occurred while updating the product"], HttpStatusCode.InternalServerError);
         }
         return ResponseDto<bool>.Success(response.IsValidResponse, [], HttpStatusCode.OK);
      }

      public async Task<ResponseDto<bool>> DeleteAsync(string id)
      {
         var response = await _productRepository.DeleteAsync(id);
         if (!response.IsValidResponse && response.Result == Elastic.Clients.Elasticsearch.Result.NotFound) return ResponseDto<bool>.Fail(["Product not found"], HttpStatusCode.NotFound);
         if (!response.IsValidResponse)
         {
            _logger.LogError("An error occurred while deleting the product: {Error}", response.TryGetOriginalException(out _));
            return ResponseDto<bool>.Fail(["An error occurred while deleting the product"], HttpStatusCode.InternalServerError);
         }
         return ResponseDto<bool>.Success(response.IsValidResponse, [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<bool>> DeleteAllAsync()
      {
         var response = await _productRepository.DeleteAllAsync();
         if (response.Total == 0) return ResponseDto<bool>.Fail(["No products found"], HttpStatusCode.NotFound);
         if (!response.IsValidResponse)
         {
            _logger.LogError("An error occurred while deleting all products: {Error}", response.TryGetOriginalException(out _));
            return ResponseDto<bool>.Fail(["An error occurred while deleting all products"], HttpStatusCode.InternalServerError);
         }
         return ResponseDto<bool>.Success(response.IsValidResponse, [], HttpStatusCode.OK);
      }
      public async Task<ResponseDto<bool>> IndexExistsAsync()
      {
         var response = await _productRepository.IndexExistsAsync();
         return ResponseDto<bool>.Success(response, [], HttpStatusCode.OK);
      }
   }
}
