using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductsController : ControllerBase
   {
      private readonly ProductService _productService;

      public ProductsController(ProductService productService)
      {
         _productService = productService;
      }
      [HttpPost]
      public async Task<IActionResult> CreateProduct(ProductCreateDto request)
      {
         var response = await _productService.SaveAsync(request);
         return StatusCode((int)response!.StatusCode, response);
      }
   }
}