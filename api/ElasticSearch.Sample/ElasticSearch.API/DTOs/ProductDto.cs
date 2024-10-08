namespace ElasticSearch.API.DTOs
{
   public record ProductDto(string Id, string Name, decimal Price, int Stock, DateTime Created, DateTime? Updated, ProductFeatureDto Features)
   {
   }
}