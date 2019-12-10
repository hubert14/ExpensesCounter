using System;
using System.Net;
using System.Threading.Tasks;
using ExpensesCounter.Common.Models;
using ExpensesCounter.Common.Models.Auth;
using ExpensesCounter.Web.BLL.Account.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesCounter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // TODO: Add logger
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            try
            {
                var tokens = await _authService.LoginAsync(model);

                return new JsonResult(tokens);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ErrorResponseModel(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpPost("login/{refreshToken}")]
        public async Task<IActionResult> LoginByRefreshTokenAsync(string refreshToken)
        {
            try
            {
                var accessToken = await _authService.LoginAsync(refreshToken);
                return new JsonResult(accessToken);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ErrorResponseModel(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            try
            {
                var tokens = await _authService.RegisterAsync(model);
                return new JsonResult(tokens);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ErrorResponseModel(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }
    }
}