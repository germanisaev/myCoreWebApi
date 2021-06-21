using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTWebApi.Models;
using JWTWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTWebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;

        public AuthController(
            IUserService userService,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        private string generateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_appSettings.JWT_Secret);
            var url = _appSettings.Issuer_URL;

            var secretKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.GivenName, user.firstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.lastName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var tokenOptions = new JwtSecurityToken(
                issuer: url,
                audience: url,
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        [HttpPost, Route("authenticate")]
        public IActionResult Login([FromBody] Login model)
        {
            var user = _userService.Authenticate(model);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            if (user != null)
            {
                return Ok(new { Token = generateJwtToken(user) });
            }
            return Unauthorized();
        }

        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] User model)
        {
            try
            {
                _userService.Create(model, model.password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpGet("{firstName}/{lastName}")]
        public IActionResult GetByName(string firstName, string lastName)
        {
            var user = _userService.GetByName(firstName, lastName);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User model)
        {
            var user = _userService.GetById(id);
            try
            {
                _userService.Update(user, model.password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}