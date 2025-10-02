using EventApp.Shared.DTOs.Auth;

namespace Backend.Services.AuthService
{
    public interface IAuthServ
    {
        public Task<string> StarRegistrationAsync(LoginRequestDto dto);
        public Task<LoginResultDto> LoginAsycn(LoginRequestDto dto);
    }
}
