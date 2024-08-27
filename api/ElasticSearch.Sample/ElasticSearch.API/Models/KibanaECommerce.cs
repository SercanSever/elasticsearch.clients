using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.MachineLearning;
using ElasticSearch.API.DTOs;

namespace ElasticSearch.API.Models.ECommerceModel
{
   public class KibanaECommerce
   {
      [JsonPropertyName("_id")]
      public string Id { get; set; } = null!;
      [JsonPropertyName("customer_first_name")]
      public string CustomerFirstName { get; set; } = null!;
      [JsonPropertyName("customer_last_name")]
      public string CustomerLastName { get; set; } = null!;
      [JsonPropertyName("customer_full_name")]
      public string CustomerFullName { get; set; } = null!;
      [JsonPropertyName("category")]
      public string[] Category { get; set; } = null!;
      [JsonPropertyName("order_id")]
      public int OrderId { get; set; }
      [JsonPropertyName("order_date")]
      public DateTime OrderDate { get; set; }
      [JsonPropertyName("taxful_total_price")]
      public double TaxfulTotalPrice { get; set; }
      [JsonPropertyName("products")]
      public KibanaECommerceProduct[] Products { get; set; } = null!;
      public KibanaECommerceDto CreateDto()
      {
         return new KibanaECommerceDto(Id, CustomerFirstName, CustomerLastName, CustomerFullName, Category, OrderId, OrderDate, TaxfulTotalPrice, Products.Select(p => new KibanaECommerceProductDto(p.ProductId, p.ProductName!)).ToArray());
      }
   }
   public class KibanaECommerceProduct
   {
      [JsonPropertyName("product_id")]
      public long ProductId { get; set; }
      [JsonPropertyName("product_name")]
      public string? ProductName { get; set; }
   }
}