using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    public class DeleteTransaction
    {
        public class Command : IRequest<bool>
        {
            public int Id { get; set; }

            public Command(int id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<DeleteTransaction.Command, bool>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                var result = await _context.Transactions.FindAsync(command.Id);
                if (result != null)
                {
                    _context.Transactions.Remove(result);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }
    }
}