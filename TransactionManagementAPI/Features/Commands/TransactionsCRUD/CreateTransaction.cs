using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    /// <summary>
    /// Class for creating new transaction
    /// </summary>
    public class CreateTransaction
    {
        public class Command : IRequest<bool>
        {
            /// <summary>
            /// Transaction for creating
            /// </summary>
            public Transaction Transaction { get; set; }

            public Command(Transaction transaction)
            {
                Transaction = transaction;
            }
        }

        public class Handler : IRequestHandler<CreateTransaction.Command, bool>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                // Creating new transaction in DataBase
                await _context.Transactions.AddAsync(command.Transaction);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}