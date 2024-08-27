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
      [HttpGet("getallwithpagination")]
      public async Task<IActionResult> GetAllWithPagination(int page, int pageSize)
      {
         var response = await _kibanaECommerceService.GetAllWithPagination(page, pageSize);
         return CreateActionResult(response);
      }
      [HttpGet("getwithwildcard")]
      public async Task<IActionResult> GetWithWildCard(string search)
      {
         var response = await _kibanaECommerceService.GetWithWildCard(search);
         return CreateActionResult(response);
      }
      [HttpGet("getwithfuzzy")]
      public async Task<IActionResult> GetWithFuzzy(string customerFirstName)
      {
         var response = await _kibanaECommerceService.GetByCustomerNameFuzzy(customerFirstName);
         return CreateActionResult(response);
      }
      [HttpGet("getbycategory")]
      public async Task<IActionResult> GetByCategory(string category)
      {
         var response = await _kibanaECommerceService.GetByCategoryMatch(category);
         return CreateActionResult(response);
      }
      [HttpGet("getbycategorymatchbool")]
      public async Task<IActionResult> GetByCategoryMatchBoolPrefix(string category)
      {
         var response = await _kibanaECommerceService.GetByCategoryMatchBoolPrefix(category);
         return CreateActionResult(response);
      }
      [HttpGet("getbycustomerfullname")]
      public async Task<IActionResult> GetByCustomerFullNameMatchPhrase(string customerFullName)
      {
         var response = await _kibanaECommerceService.GetByCustomerFullNameMatchPhrase(customerFullName);
         return CreateActionResult(response);
      }
      [HttpGet("getByCustomerCompoundQuery")]
      public async Task<IActionResult> GetByCustomerCompoundQuery(string customerFullName, string customerFirstName)
      {
         var response = await _kibanaECommerceService.GetByCustomerCompoundQuery(customerFullName, customerFirstName);
         return CreateActionResult(response);
      }
      [HttpGet("getbymultimatch")]
      public async Task<IActionResult> GetByMultiMatch(string search)
      {
         var response = await _kibanaECommerceService.GetByMultiMatch(search);
         return CreateActionResult(response);
      }
   }
}