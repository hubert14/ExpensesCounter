using System;
using System.Net;
using System.Threading.Tasks;
using ExpensesCounter.Common.Models;
using ExpensesCounter.Common.Models.Expenses;
using ExpensesCounter.Common.Models.Expenses.List;
using ExpensesCounter.Web.BLL.Expenses.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesCounter.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesService _expensesService;

        // TODO: Add logger
        public ExpensesController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetExpensesListsAsync([FromQuery] int offset = 0,
                                                               [FromQuery] int pageSize = 10)
        {
            try
            {
                var lists = await _expensesService.GetExpensesListsAsync(offset, pageSize);
                return new JsonResult(lists);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpGet("list/{listId}")]
        public async Task<IActionResult> GetExpensesListAsync([FromRoute] int listId)
        {
            try
            {
                var list = await _expensesService.GetExpensesListAsync(listId);
                return new JsonResult(list);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpPost("list")]
        public async Task<IActionResult> AddExpensesListAsync([FromBody] CreateExpensesListModel createModel)
        {
            try
            {
                var createResult = await _expensesService.AddExpensesListAsync(createModel);
                return createResult ? (IActionResult) NoContent() : BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddExpenseAsync([FromBody] AddExpenseModel addModel)
        {
            try
            {
                var createResult = await _expensesService.AddExpenseAsync(addModel);
                return createResult ? (IActionResult) NoContent() : BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpenseAsync([FromRoute] int id,
                                                            [FromBody] UpdateExpenseModel updateModel)
        {
            try
            {
                var updateResult = await _expensesService.UpdateExpenseAsync(id, updateModel);
                return updateResult ? (IActionResult) NoContent() : BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpPut("list/{id}")]
        public async Task<IActionResult> UpdateExpenseListAsync([FromRoute] int id,
                                                                [FromBody] UpdateExpensesListModel updateModel)
        {
            try
            {
                var updateResult = await _expensesService.UpdateExpensesListAsync(id, updateModel);
                return updateResult ? (IActionResult) NoContent() : BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveExpenseAsync([FromRoute] int id)
        {
            try
            {
                await _expensesService.RemoveExpenseAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }

        [HttpDelete("list/{id}")]
        public async Task<IActionResult> DeleteExpenseListAsync([FromRoute] int id)
        {
            try
            {
                await _expensesService.RemoveExpensesListAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponseModel(e.Message));
            }
        }
    }
}