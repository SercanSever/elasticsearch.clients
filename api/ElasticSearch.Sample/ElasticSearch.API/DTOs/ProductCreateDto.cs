using ElasticSearch.API.Models;

namespace ElasticSearch.API.DTOs
{
   public record ProductCreateDto(string Name, decimal Price, int Stock, ProductFeatureDto Features)
   {
      public Product CreateProduct()
      {
         return new Product
         {
            Name = Name,
            Price = Price,
            Stock = Stock,
            Feature = new ProductFeature
            {
               Width = Features.Width,
               Height = Features.Height,
               Color = (EColor)Features.Color
            }
         };
      }
   }
}