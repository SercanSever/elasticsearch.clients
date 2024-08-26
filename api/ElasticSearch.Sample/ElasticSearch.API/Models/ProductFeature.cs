using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ElasticSearch.API.Models
{
   public class ProductFeature
   {
      public int Width { get; set; }
      public int Height { get; set; }
      public EColor Color { get; set; }
   }
}