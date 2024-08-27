using System.Collections.Immutable;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.API.Models.ECommerceModel;

namespace ElasticSearch.API.Repositories
{
   public class KibanaECommerceRepository(ElasticsearchClient client)
   {
      private readonly ElasticsearchClient _client = client;
      private const string IndexName = "kibana_sample_data_ecommerce";

      public async Task<ImmutableList<KibanaECommerce>> GetCustomerByFirstNameTerm(string customerFirstName)
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
         .Query(q => q
         .Term(t => t
         .Field(f => f.CustomerFirstName.Suffix("keyword"))
         .CaseInsensitive(true)
         .Value(customerFirstName))));
         if (!response.IsValidResponse) return [];
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return [.. response.Documents];
      }
      public async Task<ImmutableList<KibanaECommerce>> GetCustomerByFirstNamesTerms(List<string> customerFirstNameList)
      {
         var terms = new List<FieldValue>();
         foreach (var customerFirstName in customerFirstNameList)
         {
            terms.Add(customerFirstName);
         }
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
         .Size(100)
         .Query(q => q
         .Terms(t => t
         .Field(f => f.CustomerFirstName.Suffix("keyword"))
         .Term(new TermsQueryField(terms.AsReadOnly())))));
         if (!response.IsValidResponse) return [];
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return [.. response.Documents];
      }
      public async Task<ImmutableList<KibanaECommerce>> GetCustomerByFullNamePrefix(string customerFullNamePrefix)
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
          .Query(q => q
          .Prefix(p => p
          .Field(f => f.CustomerFullName.Suffix("keyword"))
          .Value(customerFullNamePrefix))));
         if (!response.IsValidResponse) return [];
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return [.. response.Documents];
      }
      public async Task<ImmutableList<KibanaECommerce>> GetProductsByTexfulTotalPriceRange(double minPrice, double maxPrice)
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
           .Query(q => q
           .Range(r => r
           .NumberRange(nr => nr
           .Field(f => f.TaxfulTotalPrice)
           .Gte(minPrice)
           .Lte(maxPrice)))));
         if (!response.IsValidResponse) return [];
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return [.. response.Documents];
      }
      public async Task<ImmutableList<KibanaECommerce>> GetAll()
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
            .Size(100));
         if (!response.IsValidResponse) return [];
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return [.. response.Documents];
      }
      public async Task<ImmutableList<KibanaECommerce>> GetAllWithPagination(int page, int pageSize)
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
             .From((page - 1) * pageSize)
             .Size(pageSize));
         if (!response.IsValidResponse) return [];
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return [.. response.Documents];
      }
      public async Task<ImmutableList<KibanaECommerce>> GetWithWildCard(string search)
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
         .Query(q => q.Wildcard(w => w.Field(f => f.CustomerFullName.Suffix("keyword")).Wildcard(search))));
         if (!response.IsValidResponse) return [];
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return [.. response.Documents];
      }
      public async Task<ImmutableList<KibanaECommerce>> GetByCustomerNameFuzzy(string customerName)
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
         .Query(q => q
         .Fuzzy(f => f
         .Field(f => f.CustomerFirstName.Suffix("keyword")).Value(customerName).Fuzziness(new Fuzziness(3)))).Sort(so => so.Field(f => f.TaxfulTotalPrice, new FieldSort() { Order = SortOrder.Desc })));
         if (!response.IsValidResponse) return [];
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return [.. response.Documents];
      }
      public async Task<ImmutableList<KibanaECommerce>> GetByCategoryMatch(string categoryName)
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
        .Size(1000)
        .Query(q => q
        .Match(m => m
        .Field(f => f.Category)
        .Query(categoryName))));
         if (!response.IsValidResponse) return [];
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return [.. response.Documents];
      }
   }
}
