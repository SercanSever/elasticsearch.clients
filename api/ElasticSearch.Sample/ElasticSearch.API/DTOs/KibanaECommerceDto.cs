using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.API.DTOs
{
   public record KibanaECommerceDto(string Id, string CustomerFirstName, string CustomerLastName, string CustomerFullName, string[] Category, int OrderId, DateTime OrderDate, KibanaECommerceProductDto[] Products)
   {

   }
}