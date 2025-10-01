using NETReact.Shared.Models;

namespace Backend.Services.JWTService
{
    public interface IJWTServ
    {
        string GenerateToken(User user);
    }
}
