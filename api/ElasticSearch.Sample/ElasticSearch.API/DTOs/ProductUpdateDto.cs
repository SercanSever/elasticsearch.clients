using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearch.API.Models;

namespace ElasticSearch.API.DTOs
{
   public record ProductUpdateDto(string Id, string Name, decimal Price, int Stock, ProductFeatureDto Features)
   {
   }
}