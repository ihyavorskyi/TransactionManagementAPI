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
    /// <summary>
    /// Used to manage transaction in the system.
    /// Сan be used only by authorized users.
    /// </summary>
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

        /// <summary> Get clients by name </summary>
        /// <param name="name"> Client name </param>
        /// <returns> List of client whose name contains specifited name </returns>
        [HttpGet("client/{name}")]
        public async Task<IActionResult> GeClientByNameAsync(string name)
        {
            var query = new GetClientByName.Query(name);
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        [HttpGet("getFiltered/{filters}")]
        public async Task<IActionResult> GetFilteredAsync(int filters)
        {
            var query = new GetFilteredTransactions.Query(filters);
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        /// <summary>
        /// Method causes parsing of Csv file,
        /// and adds them to the database using merge on id transaction
        /// (if the transaction exists, the status is updated).
        /// </summary>
        /// <param name="fileName"> Name of the file that contains the transaction </param>
        /// <returns> Execution status </returns>
        [HttpPost("exportFromCsv/{fileName}")]
        public async Task<IActionResult> ExportFromCsvAsync(string fileName)
        {
            var command = new AddCollectionOfTransactions.Command(fileName);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost("exportToExcel")]
        public async Task<IActionResult> ExportToExcelAsync(ExcelExportSettings options)
        {
            var command = new ExportToExcel.Command(options);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        /// <summary> Edit status an existing transaction </summary>
        /// <remarks> This can only be done by an authorized user<br/>
        /// Numerical representations of statuses : <br/>Pending = 1, <br/>Completed = 2, <br/>Cancelled = 4
        /// </remarks>
        /// <param name="id"> Transaction ID </param>
        /// <param name="status"> New transaction status </param>
        /// <returns> Execution status </returns>
        [HttpPut("status")]
        public async Task<IActionResult> EditStatusAsync(int id, TransactionStatus status)
        {
            var command = new EditTransactionStatus.Command(new EditTransactionDto() { Id = id, Status = status });
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        /// <summary> Delete transaction by ID </summary>
        /// <remarks> This can only be done by an authorized user </remarks>
        /// <param name="id"> Transaction id to delete </param>
        /// <returns> Execution status </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteTransaction.Command(id);
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}