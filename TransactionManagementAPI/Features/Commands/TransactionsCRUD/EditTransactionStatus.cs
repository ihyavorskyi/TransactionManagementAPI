using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.DTOs;
using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    public class EditTransactionStatus
    {
        public class Command : IRequest<Response>
        {
            public EditTransactionDto TransactionDto { get; set; }

            public Command(EditTransactionDto transactionDto)
            {
                TransactionDto = transactionDto;
            }
        }

        public class Handler : IRequestHandler<EditTransactionStatus.Command, Response>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
            {
                if (Enum.IsDefined(typeof(TransactionStatus), command.TransactionDto.Status))
                {
                    return new Response() { Status = "Error", Message = "Wrong status. Available statuses:  Pending = 0, Completed = 1, Cancelled = 2" };
                }

                var transaction = await _context.Transactions.FindAsync(command.TransactionDto.Id);
                if (transaction != null)
                {
                    transaction.Status = command.TransactionDto.Status;
                    await _context.SaveChangesAsync();
                    return new Response() { Status = "Successfully", Message = "Status changed successfully" };
                }

                return new Response() { Status = "Error", Message = "Status not changed" };
            }
        }
    }
}