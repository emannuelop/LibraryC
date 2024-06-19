using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using LibraryC.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public string GenerateToken(Usuario usuario)
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
                    new Claim(type: ClaimTypes.Name, user.Email),
                    new Claim(type: ClaimTypes.Role, user.Perfil)
                ],
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signinCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;

        }

        public string HashPassword(string password)
        {

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password), "A senha não pode ser nula.");
            }

            using (SHA256 sha256 = SHA256.Create())
            {

                // ComputeHash - retorna um array de bytes
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertendo bytes para uma string hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {

            // Hash a senha de entrada e compare-a com a senha já hasheada
            string hashedInput = HashPassword(password);

            if (hashedInput == null)
            {
                // Inicializar hashedInput ou lançar uma exceção apropriada
                throw new InvalidOperationException("hashedInput não foi inicializada.");
            }
            return hashedInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
