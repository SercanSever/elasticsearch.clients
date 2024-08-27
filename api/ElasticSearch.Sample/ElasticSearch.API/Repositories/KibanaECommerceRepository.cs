using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
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
      public async Task<ImmutableList<KibanaECommerce>> TermQuesry(string customerFirstName)
      {
         var result = await _client.SearchAsync<KibanaECommerce>(x => x.Query(q => q.Term(t => t.Field("customer_first_name.keyword"!).Value(customerFirstName))));
         return result.Documents.ToImmutableList();
      }
   }
}