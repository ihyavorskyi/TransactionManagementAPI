using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.DTOs;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    public class DeleteTransaction
    {
        public class Command : IRequest<Response>
        {
            public int Id { get; set; }

            public Command(int id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<DeleteTransaction.Command, Response>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
            {
                var result = await _context.Transactions.FindAsync(command.Id);
                if (result != null)
                {
                    _context.Transactions.Remove(result);
                    await _context.SaveChangesAsync();
                    return new Response() { Status = "Successfully", Message = "Trasnaction successfully deleted" };
                }
                return new Response() { Status = "Not found", Message = "Trasnaction not found" };
            }
        }
    }
}