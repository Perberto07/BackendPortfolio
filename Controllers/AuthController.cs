using Backend.Services.AuthService;
using Backend.Services.JWTService;
using EventApp.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServ _auth;

        public AuthController(IAuthServ auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registeruser(LoginRequestDto dto)
        {
            try
            {
                var token = await _auth.StarRegistrationAsync(dto);
                 return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var result = await _auth.LoginAsycn(dto);

            if(!result.Success)
                return Unauthorized(result.ErrorMessage);

            return Ok(result);
        }

    }
}
