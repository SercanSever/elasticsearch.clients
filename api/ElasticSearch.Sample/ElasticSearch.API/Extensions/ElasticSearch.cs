using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace ElasticSearch.API.Extensions
{
   public static class ElasticSearch
   {
      public static void AddElastic(this IServiceCollection serviceColelction, IConfiguration configuration)
      {
         var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("ElasticSearch:Url").Value!));
         var settings = new ConnectionSettings(pool);
         settings.BasicAuthentication(configuration.GetSection("ElasticSearch:Username").Value!, configuration.GetSection("ElasticSearch:Password").Value!);
         var client = new ElasticClient(settings);
         serviceColelction.AddSingleton(client);
      }
   }
}