using ElasticSearch.API.DTOs;
using ElasticSearch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductsController : BaseController
   {
      private readonly ProductService _productService;

      public ProductsController(ProductService productService)
      {
         _productService = productService;
      }
      [HttpPost("create")]
      public async Task<IActionResult> CreateProduct(ProductCreateDto request)
      {
         var response = await _productService.SaveAsync(request);
         return CreateActionResult(response!);
      }
      [HttpGet("getall")]
      public async Task<IActionResult> GetAllProducts()
      {
         var response = await _productService.GetAllAsync();
         return CreateActionResult(response!);
      }
      [HttpGet("getbyid")]
      public async Task<IActionResult> GetProductById(string id)
      {
         var response = await _productService.GetByIdAsync(id);
         return CreateActionResult(response!);
      }
      [HttpDelete("delete")]
      public async Task<IActionResult> DeleteProduct(string id)
      {
         var response = await _productService.DeleteAsync(id);
         return CreateActionResult(response!);
      }
      [HttpDelete("deleteall")]
      public async Task<IActionResult> DeleteAllProducts()
      {
         var response = await _productService.DeleteAllAsync();
         return CreateActionResult(response!);
      }
      [HttpGet("indexexists")]
      public async Task<IActionResult> IndexExists()
      {
         var response = await _productService.IndexExistsAsync();
         return CreateActionResult(response!);
      }
   }
}