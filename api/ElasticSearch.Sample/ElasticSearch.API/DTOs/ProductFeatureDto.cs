using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearch.API.Models;

namespace ElasticSearch.API.DTOs
{
   public record ProductFeatureDto(int Width, int Height, EColor Color)
   {
   }
}