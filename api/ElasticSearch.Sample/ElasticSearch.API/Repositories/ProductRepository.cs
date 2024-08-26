using ElasticSearch.API.Models;
using Nest;

namespace ElasticSearch.API.Repositories
{
   public class ProductRepository
   {
      private readonly string _indexName = "products";
      private readonly ElasticClient _client;
      public ProductRepository(ElasticClient client)
      {
         _client = client;
      }
      public async Task<Product?> SaveAsync(Product product)
      {
         product.Created = DateTime.Now;
         var response = await _client.IndexAsync(product, idx => idx.Index(_indexName).Id(Guid.NewGuid().ToString()));
         if (!response.IsValid) return null;
         product.Id = response.Id;
         return product;
      }
      public async Task<IReadOnlyCollection<Product?>> GetAllAsync()
      {
         var response = await _client.SearchAsync<Product>(s => s.Index(_indexName));
         if (!response.IsValid) return Array.Empty<Product>();
         foreach (var hit in response.Hits) hit.Source.Id = hit.Id;
         return response.Documents;
      }
      public async Task<Product?> GetByIdAsync(string id)
      {
         var response = await _client.GetAsync<Product>(id, idx => idx.Index(_indexName));
         if (!response.IsValid) return null;
         response.Source.Id = response.Id;
         return response.Source;
      }
      public async Task<bool> UpdateAsync(Product product)
      {
         product.Updated = DateTime.Now;
         var response = await _client.UpdateAsync<Product>(product.Id, u => u.Index(_indexName).Doc(product));
         return response.IsValid;
      }
      public async Task<bool> DeleteAsync(string id)
      {
         var response = await _client.DeleteAsync<Product>(id, idx => idx.Index(_indexName));
         return response.IsValid;
      }
      public async Task<bool> DeleteAllAsync()
      {
         var response = await _client.DeleteByQueryAsync<Product>(q => q.Index(_indexName).Query(q => q.MatchAll()));
         return response.IsValid;
      }
      public async Task<bool> IndexExistsAsync()
      {
         var response = await _client.Indices.ExistsAsync(_indexName);
         return response.Exists;
      }

   }
}