using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.DTOs;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    /// <summary>
    /// Class for editing transaction status
    /// </summary>
    public class EditTransactionStatus
    {
        public class Command : IRequest<string>
        {
            /// <summary>
            /// Model with new status and id of transaction
            /// </summary>
            public EditTransactionDto TransactionDto { get; set; }

            public Command(EditTransactionDto transactionDto)
            {
                TransactionDto = transactionDto;
            }
        }

        public class Handler : IRequestHandler<EditTransactionStatus.Command, string>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(Command command, CancellationToken cancellationToken)
            {
                // Finding transaction and changing them status
                var transaction = await _context.Transactions.FindAsync(command.TransactionDto.Id);
                if (transaction != null)
                {
                    transaction.Status = command.TransactionDto.Status;
                    await _context.SaveChangesAsync();
                    return "Status changed successfully";
                }

                return "Transaction not found";
            }
        }
    }
}