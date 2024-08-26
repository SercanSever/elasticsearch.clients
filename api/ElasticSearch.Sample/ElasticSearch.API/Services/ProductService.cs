using System.Net;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Repositories;

namespace ElasticSearch.API.Services
{
   public class ProductService
   {
      private readonly ProductRepository _productRepository;
      public ProductService(ProductRepository productRepository)
      {
         _productRepository = productRepository;
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
         return ResponseDto<ProductDto>.Success(response.CreateDto(), new List<string>(), HttpStatusCode.OK);
      }

      public async Task<ResponseDto<bool>> DeleteAsync(string id)
      {
         var response = await _productRepository.DeleteAsync(id);
         return ResponseDto<bool>.Success(response, new List<string>(), HttpStatusCode.OK);
      }
      public async Task<ResponseDto<bool>> DeleteAllAsync()
      {
         var response = await _productRepository.DeleteAllAsync();
         return ResponseDto<bool>.Success(response, new List<string>(), HttpStatusCode.OK);
      }
      public async Task<ResponseDto<bool>> IndexExistsAsync()
      {
         var response = await _productRepository.IndexExistsAsync();
         return ResponseDto<bool>.Success(response, new List<string>(), HttpStatusCode.OK);
      }
   }
}
