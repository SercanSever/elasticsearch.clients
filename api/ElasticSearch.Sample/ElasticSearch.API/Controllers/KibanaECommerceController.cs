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
   }
}