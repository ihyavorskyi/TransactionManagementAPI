using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.DTOs;
using TransactionManagementAPI.Data.Enums;
using TransactionManagementAPI.Features;
using TransactionManagementAPI.Features.Commands.TransactionsCRUD;
using TransactionManagementAPI.Features.Query.TransactionsCRUD;

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

        [HttpGet("getFiltered/{filters}")]
        public async Task<IActionResult> GetFilteredAsync(TransactionFilters filters)
        {
            var query = new GetFilteredTransactions.Query(filters);
            var res = await _mediator.Send(query);
            return Ok(res);
        }

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
            var command = new AddCollectionOfTransactions.Command(fileName);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost("exportToExcel")]
        public async Task<IActionResult> ExportToExcelAsync(ExcelExportOptions options)
        {
            var command = new ExportToExcel.Command(options);
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