using ElasticSearch.API.Models;
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
      public async Task<Product?> SaveAsync(Product product)
      {
         return await _productRepository.SaveAsync(product);
      }
   }
}