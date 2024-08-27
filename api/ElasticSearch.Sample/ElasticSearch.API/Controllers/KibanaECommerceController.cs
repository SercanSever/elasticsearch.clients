using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearch.API.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers
{
   public class KibanaECommerceController : BaseController
   {
      private readonly KibanaECommerceService _kibanaECommerceService;

      public KibanaECommerceController(KibanaECommerceService kibanaECommerceService)
      {
         _kibanaECommerceService = kibanaECommerceService;
      }
      [HttpGet("getbyterm")]
      public async Task<IActionResult> Get(string customerFirstName)
      {
         var response = await _kibanaECommerceService.GetCustomerByFirstName(customerFirstName);
         return CreateActionResult(response);
      }
      [HttpPost("getbyterms")]
      public async Task<IActionResult> Get(List<string> customerFirstNameList)
      {
         var response = await _kibanaECommerceService.GetCustomerByFirstNames(customerFirstNameList);
         return CreateActionResult(response);
      }
      [HttpGet("getbyfullname")]
      public async Task<IActionResult> GetByFullName(string customerFullNamePrefix)
      {
         var response = await _kibanaECommerceService.GetCustomerByFullName(customerFullNamePrefix);
         return CreateActionResult(response);
      }
      [HttpGet("getbypricerange")]
      public async Task<IActionResult> GetByPriceRange(double minPrice, double maxPrice)
      {
         var response = await _kibanaECommerceService.GetProductsByTotalPriceRange(minPrice, maxPrice);
         return CreateActionResult(response);
      }
      [HttpGet("getall")]
      public async Task<IActionResult> GetAll()
      {
         var response = await _kibanaECommerceService.GetAll();
         return CreateActionResult(response);
      }
   }
}