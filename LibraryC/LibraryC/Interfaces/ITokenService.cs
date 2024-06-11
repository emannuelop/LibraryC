using LibraryC.DTOs;

namespace LibraryC.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(LoginDTO usuario);
    }
}
