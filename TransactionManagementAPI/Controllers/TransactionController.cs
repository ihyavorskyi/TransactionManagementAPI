using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.DTOs;
using TransactionManagementAPI.Data.TransactionFilters;
using TransactionManagementAPI.Features.Commands.TransactionsCRUD;
using TransactionManagementAPI.Features.Query.TransactionsCRUD;
using TransactionManagementAPI.Features.Query.WorkWithCsv;

namespace TransactionManagementAPI.Controllers
{
    // [Authorize]
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getFiltered/{id}/{id1}")]
        public async Task<IActionResult> GetFilteredAsync(bool id, bool id1)
        {
            var query = new GetFilteredTransactions.Query();
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        //[HttpGet("client")]
        //public async Task<IActionResult> GetAsync()
        //{
        //    var query = new GetAllTransactions.Query();
        //    var res = await _mediator.Send(query);
        //    return Ok(res);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Transaction transaction)
        {
            var command = new CreateTransaction.Command(transaction);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost("exportFromCsv/{fileName}")]
        public async Task<IActionResult> ExportFromCsvAsync(string fileName)
        {
            var fromCsv = new WorkWithCsv(fileName);
            var transactions = fromCsv.Read();

            var command = new AddCollectionOfTransactions.Command(transactions);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPut("status")]
        public async Task<IActionResult> EditStatusAsync(EditTransactionDto article)
        {
            var command = new EditTransactionStatus.Command(article);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteTransaction.Command(id);
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}