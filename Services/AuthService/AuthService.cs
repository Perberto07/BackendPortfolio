using Backend.Data;
using Backend.Services.JWTService;
using EventApp.Shared.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using NETReact.Shared.Models;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Services.AuthService
{
    public class AuthService : IAuthServ
    {
        private readonly DataContext _ctx;
        private readonly IJWTServ _jwt;

        public AuthService(DataContext ctx, IJWTServ jwt)
        {
            _ctx = ctx;
            _jwt = jwt;
        }


        public async Task<LoginResultDto> LoginAsycn(LoginRequestDto dto)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Name == dto.Name);

            if (user == null)
                return new LoginResultDto { Success = false, ErrorMessage = "Username not found." };

            if (!VerifyPassword(dto.Password, user.Password))
                return new LoginResultDto { Success = false, ErrorMessage = "incorrect Password" };

            return new LoginResultDto
            {
                Success = true,
                Token = _jwt.GenerateToken(user)
            };

        }

        public async Task<string> StarRegistrationAsync(LoginRequestDto dto)
        {
           var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Name == dto.Name);

           if(user != null) throw new InvalidOperationException("Email already used.");

            var newuser = new User
            {
                Name = dto.Name,
                Password = HashPassword(dto.Password)
            };

            _ctx.Users.Add(newuser);
            await _ctx.SaveChangesAsync();

            return _jwt.GenerateToken(newuser);
        }
        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(sha.ComputeHash(bytes));
        }
        private static bool VerifyPassword(string password, string hashed)
        {
            return HashPassword(password) == hashed;
        }


    }
}
