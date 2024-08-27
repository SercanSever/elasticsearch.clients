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
         var result = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName).Query(q => q.Term(t => t.Field(f => f.CustomerFirstName.Suffix("keyword")).CaseInsensitive(true).Value(customerFirstName))));
         foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;
         return result.Documents.ToImmutableList();
      }
      public async Task<ImmutableList<KibanaECommerce>> GetCustomerByFirstNamesTerm(List<string> customerFirstNameList)
      {
         var terms = new List<FieldValue>();
         foreach (var customerFirstName in customerFirstNameList)
         {
            terms.Add(customerFirstName);
         }

         var termsQuery = new TermsQuery()
         {
            Field = "customer_first_name.keyword"!,
            Term = new TermsQueryField(terms.AsReadOnly()),
         };
         var result = await _client.SearchAsync<KibanaECommerce>(s => s.Index(IndexName).Query(q => q.Terms(termsQuery)));
         foreach (var hit in result.Hits) hit.Source!.Id = hit.Id!;
         return result.Documents.ToImmutableList();
      }
   }
}
