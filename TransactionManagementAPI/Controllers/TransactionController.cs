using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Features.Commands.TransactionsCRUD;

namespace TransactionManagementAPI.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Transaction transaction)
        {
            var command = new CreateTransaction.Command(transaction);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        //[HttpPost("readAndWrite/{fileName}")]
        //public async Task<IActionResult> ReadAndWriteTransactionsAsync(string fileName)
        //{
        //    var updCommand = new ReadAndWriteTransactions.Command(fileName);
        //    var res = await _mediator.Send(updCommand);
        //    return Ok(res);
        //}

        //[HttpPut]
        //public async Task<IActionResult> EditAsync(TransactionModel article)
        //{
        //    var command = new EditTransaction.Command(article);
        //    var res = await _mediator.Send(command);
        //    return Ok(res);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteTransaction.Command(id);
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}