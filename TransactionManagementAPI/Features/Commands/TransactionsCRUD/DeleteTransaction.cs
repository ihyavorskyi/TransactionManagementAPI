using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.DTOs;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    /// <summary>
    /// Class for deleting transaction
    /// </summary>
    public class DeleteTransaction
    {
        public class Command : IRequest<string>
        {
            /// <summary>
            /// Transaction id for deleting
            /// </summary>
            public int Id { get; set; }

            public Command(int id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<DeleteTransaction.Command, string>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(Command command, CancellationToken cancellationToken)
            {
                // If transaction exist delete him
                var result = await _context.Transactions.FindAsync(command.Id);
                if (result != null)
                {
                    _context.Transactions.Remove(result);
                    await _context.SaveChangesAsync();
                    return "Trasnaction successfully deleted";
                }
                return "Trasnaction not found";
            }
        }
    }
}