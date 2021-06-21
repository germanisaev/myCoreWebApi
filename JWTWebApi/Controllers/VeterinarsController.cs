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
    public class VeterinarsController : ControllerBase
    {
        private IVeterinarService _vetService;

        public VeterinarsController(
            IVeterinarService vetService)
        {
            _vetService = vetService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _vetService.GetAll();
            return Ok(result);
        }

        // GET api/veterinars/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _vetService.GetById(id);
            return Ok(result);
        }

        // POST api/veterinars
        [HttpPost]
        public IActionResult Post([FromBody] Veterinar model)
        {
            try
            {
                _vetService.Create(model);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/veterinars/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Veterinar model)
        {
            try
            {
                _vetService.Update(id, model);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/veterinars/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _vetService.Delete(id);
            return Ok();
        }
    }
}