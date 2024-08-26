using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ElasticSearch.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class BaseController : ControllerBase
   {
      [NonAction]
      public IActionResult CreateActionResult<T>(ResponseDto<T> response)
      {
         if (response.StatusCode == HttpStatusCode.NoContent)
            return new ObjectResult(null) { StatusCode = response.StatusCode.GetHashCode() };

         return new ObjectResult(response) { StatusCode = response.StatusCode.GetHashCode() };
      }
   }
}