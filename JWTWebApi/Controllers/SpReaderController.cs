using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTWebApi.Models;
using JWTWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpReaderController : ControllerBase
    {
        private readonly IQueueService _context;

        public SpReaderController(IQueueService context)
        {
            _context = context;
        }

        [HttpPost("exist")]
        public async Task<ActionResult<int>> GetExist([FromBody] Grooming model)
        {
            var result = await _context.CreateQueueAsync(model);
            return Ok(result);
        }

    }
}