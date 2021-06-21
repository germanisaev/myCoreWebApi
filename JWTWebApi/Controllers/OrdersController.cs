using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        //[Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        }
    }
}
