using System.Collections.Immutable;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.API.Models.ECommerceModel;

namespace ElasticSearch.API.Repositories
{
   public class KibanaECommerceRepository
   {
      private readonly ElasticsearchClient _client;
      private const string IndexName = "kibana_sample_data_ecommerce";
      public KibanaECommerceRepository(ElasticsearchClient client)
      {
         _client = client;
      }
      public async Task<ImmutableList<KibanaECommerce>> GetCustomerByFirstNameTerm(string customerFirstName)
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
         .Query(q => q
         .Term(t => t
         .Field(f => f.CustomerFirstName.Suffix("keyword"))
         .CaseInsensitive(true)
         .Value(customerFirstName))));
         if (!response.IsValidResponse) return ImmutableList<KibanaECommerce>.Empty;
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return response.Documents.ToImmutableList();
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
         if (!response.IsValidResponse) return ImmutableList<KibanaECommerce>.Empty;
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return response.Documents.ToImmutableList();
      }
      public async Task<ImmutableList<KibanaECommerce>> GetCustomerByFullNamePrefix(string customerFullNamePrefix)
      {
         var response = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName)
          .Query(q => q
          .Prefix(p => p
          .Field(f => f.CustomerFullName.Suffix("keyword"))
          .Value(customerFullNamePrefix))));
         if (!response.IsValidResponse) return ImmutableList<KibanaECommerce>.Empty;
         foreach (var hit in response.Hits) hit.Source!.Id = hit.Id!;
         return response.Documents.ToImmutableList();
      }
   }
}
