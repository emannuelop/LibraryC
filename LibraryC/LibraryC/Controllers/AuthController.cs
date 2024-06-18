using Azure.Core;
using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Common;
using NuGet.Protocol;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace LibraryC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(ITokenService tokenService, IUsuarioRepository usuarioRepository)
        {
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login( LoginDTO login)
        {

            var usuario = _usuarioRepository.SelecionarPorEmail(login.Email);

            if (usuario ==  null)
            {
                return Unauthorized();
            }

            if (!_tokenService.VerifyPassword(login.Senha, usuario.Senha))
            {
                // Retorna o usuário se a senha estiver correta
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(usuario);

            Response.Headers.Append("Authorization", token);

            return Ok(usuario);
        }

    }

    public static class HttpResponseExtensions
    {
        public static IActionResult WithHeader(this IActionResult result, string header, string value)
        {
            if (result is ObjectResult objectResult)
            {
                objectResult.WithHeader(header, value);
            }
            return result;
        }
    }

}
