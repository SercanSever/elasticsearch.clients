using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElasticSearch.API.Extensions
{
   public static class ElasticSearchExtension
   {
      public static void AddElastic(this IServiceCollection serviceColelction, IConfiguration configuration)
      {
         // var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("ElasticSearch:Url").Value!));
         var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("ElasticSearch:Url").Value!)).Authentication(new BasicAuthentication(configuration.GetSection("ElasticSearch:Username").Value!, configuration.GetSection("ElasticSearch:Password").Value!));
         var client = new ElasticsearchClient(settings);
         serviceColelction.AddSingleton(client);
      }
   }
}