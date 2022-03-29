using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookApi.Interfaces;
using BookApi.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationServive _authentication;

        public AccountController(IConfiguration configuration, IAuthenticationServive authentication)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
        }

        [HttpPost]
        [Route("/api/user/create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterInterface model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfimPassword", "As senhas não conferem");

                return BadRequest(ModelState);
            }

            var result = await _authentication.ResgisterUser(model.UserName, model.Email, model.Password);

            if (result)
            {
                return Ok($"O usuário {model.UserName} foi criado com sucesso!");
            }
            else
            {
                ModelState.AddModelError("CreateUser", "Registro inválido.");

                return BadRequest(ModelState);
            }

        }

        [HttpPost]
        [Route("/api/user/signin")]
        public ActionResult<UserToken> Login([FromBody] LoginInterface model)
        {
            var result = _authentication.Authentication(model.Email, model.Password);

            if (result)
            {
                var user = _authentication.GetUser(model.Email);
                var info = GeneretedToken(model);

                return Ok(new { info, user });
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Login inválido.");

                return BadRequest(ModelState);
            }
        }

        public ActionResult<UserToken> GeneretedToken([FromBody] LoginInterface model)
        {
            var claims = new[]
            {
                new Claim("email", model.Email),
                new Claim("meuToken", "token do iago"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.Now.AddMinutes(20);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }

}