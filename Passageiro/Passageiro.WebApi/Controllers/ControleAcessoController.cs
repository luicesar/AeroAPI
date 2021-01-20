using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Passageiro.Repository.Models;
using Passageiro.Service.Interfaces;
using Passageiro.WebApi.ViewModels;

namespace Passageiro.WebApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ControleAcessoController : ControllerBase {

        private readonly ILogger<ControleAcessoController> _logger;
        private readonly IControleAcessoService _service;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }

        public ControleAcessoController (
            ILogger<ControleAcessoController> logger,
            IControleAcessoService service,
            IMapper mapper, IConfiguration configuration) {
            this._logger = logger;
            this._service = service;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route ("autenticacao")]
        [AllowAnonymous]
        public IActionResult Autenticacao ([FromBody] UsuarioAuth user) {

            try {
                var autenticacao = _service.Authenticate (user.Login, user.Senha);
                var usuario = _mapper.Map<UsuarioLogadoModel> (autenticacao);

                if (autenticacao == null)
                    return BadRequest (new { errors = "Usuário ou Senha Inválido." });

                return Ok (new {
                    token = GenerationJWTToken (usuario),
                        user = usuario
                });
            } catch (Exception exc) {
                return StatusCode (500, exc.Message);
            }
        }

        private string GenerationJWTToken (UsuarioLogadoModel usuario) {

            var key = Encoding.ASCII.GetBytes (_configuration["JWTSettings:Secret"]);

            var claims = new Claim[] {
                //new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim (ClaimTypes.Name, usuario.Login),
                new Claim (ClaimTypes.Role, usuario.Rules)
            };

            var credentials = new SigningCredentials (
                new SymmetricSecurityKey (key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler ();
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (3),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken (tokenDescriptor);

            return new JwtSecurityTokenHandler ().WriteToken (token);
        }

    }
}