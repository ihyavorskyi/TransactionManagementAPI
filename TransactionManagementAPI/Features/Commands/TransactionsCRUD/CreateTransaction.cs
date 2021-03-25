using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TouristClubApi.Data;
using TransactionManagementAPI.Data;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    public class CreateTransaction
    {
        public class Command : IRequest<bool>
        {
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
                await _context.Transactions.AddAsync(command.Transaction);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}