using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;

namespace ElasticSearch.API.Repositories
{
   public class ProductRepository
   {
      private readonly string _indexName = "products";
      private readonly ElasticsearchClient _client;
      public ProductRepository(ElasticsearchClient client)
      {
         _client = client;
      }
      public async Task<Product?> SaveAsync(Product product)
      {
         product.Created = DateTime.Now;
         var response = await _client.IndexAsync(product, idx => idx.Index(_indexName).Id(Guid.NewGuid().ToString()));
         if (!response.IsValidResponse) return null;
         product.Id = response.Id;
         return product;
      }
      public async Task<IReadOnlyCollection<Product?>> GetAllAsync()
      {
         var response = await _client.SearchAsync<Product>(s => s.Index(_indexName));
         if (!response.IsValidResponse) return Array.Empty<Product>();
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return response.Documents;
      }
      public async Task<GetResponse<Product>?> GetByIdAsync(string id)
      {
         var response = await _client.GetAsync<Product>(id, idx => idx.Index(_indexName));
         if (!response.IsValidResponse) return null;
         response.Source!.Id = response.Id;
         return response;
      }
      public async Task<UpdateResponse<Product>> UpdateAsync(ProductUpdateDto productUpdateDto)
      {
         var response = await _client.UpdateAsync<Product, ProductUpdateDto>(_indexName, productUpdateDto.Id, x => x.Doc(productUpdateDto));
         return response;
      }
      public async Task<DeleteResponse> DeleteAsync(string id)
      {
         var response = await _client.DeleteAsync<Product>(id, idx => idx.Index(_indexName));
         return response;
      }
      public async Task<DeleteByQueryResponse> DeleteAllAsync()
      {
         var response = await _client.DeleteByQueryAsync<Product>(_indexName, q => q.Query(q => q.Ids(i => i.Values("*"))));
         return response!;
      }
      public async Task<bool> IndexExistsAsync()
      {
         var response = await _client.Indices.ExistsAsync(_indexName);
         return response.Exists;
      }

   }
}