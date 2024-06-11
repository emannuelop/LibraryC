using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using LibraryC.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryC.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public TokenService(IConfiguration configuration, IUsuarioRepository usuarioRepository) {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        public string GenerateToken(LoginDTO usuario)
        {
            var user = _usuarioRepository.SelecionarPorEmail(usuario.Email);
            if (user == null)
            {
                return String.Empty;
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims:
                [
                    new Claim(type: ClaimTypes.Name, user.Nome),
                    new Claim(type: ClaimTypes.Role, user.Perfil)
                ],
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signinCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;

        }
    }
}
