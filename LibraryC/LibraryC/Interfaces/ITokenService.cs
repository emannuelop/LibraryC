
using LibraryC.Models;

namespace LibraryC.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);

        string HashPassword(string password);

        bool VerifyPassword(string password, string hashedPassword);
    }
}
