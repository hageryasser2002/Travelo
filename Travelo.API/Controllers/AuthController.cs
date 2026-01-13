using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travelo.Application.Common.Responses;
using System.Security.Claims;
using Travelo.Application.DTOs.Auth;
using Travelo.Application.Interfaces;
using Travelo.Application.UseCases.Auth;

namespace Travelo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDTO registerDTO,
            [FromServices] RegisterUseCase registerUseCase
            )
        {
            var result = await registerUseCase.ExecuteAsync(registerDTO);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDTO loginDTO,
            [FromServices] LoginUseCase loginUseCase)
        {
            var result = await loginUseCase.ExecuteAsync(loginDTO);

            if (!result.Success)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
    


        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin(
            [FromBody] GoogleLoginDTO dto,
            [FromServices] GoogleLoginUseCase useCase)
        {
            var result = await useCase.ExecuteAsync(dto);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }




    }
}
