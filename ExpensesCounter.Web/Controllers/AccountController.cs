using System;
using System.Net;
using System.Threading.Tasks;
using ExpensesCounter.Common.Models;
using ExpensesCounter.Common.Models.User;
using ExpensesCounter.Web.BLL;
using ExpensesCounter.Web.BLL.Account.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesCounter.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        // TODO: Add logger
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfoAsync()
        {
            try
            {
                var info = await _accountService.GetInfoAsync();
                return new JsonResult(info);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserInfoAsync([FromBody] UpdateUserInfoModel updateModel)
        {
            try
            {
                var updateResult = await _accountService.UpdateInfoAsync(updateModel);
                return updateResult ? (IActionResult) NoContent() : BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpPut("enabling")]
        public async Task<IActionResult> ChangeEnableStatusAsync()
        {
            try
            {
                var updateResult = await _accountService.ChangeEnableStateAsync();
                return updateResult ? (IActionResult) NoContent() : BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }
    }
}