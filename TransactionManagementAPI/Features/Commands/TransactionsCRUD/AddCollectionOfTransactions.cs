using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    public class AddCollectionOfTransactions
    {
        public class Command : IRequest<bool>
        {
            public IEnumerable<Transaction> Transactions { get; set; }

            public Command(IEnumerable<Transaction> transactions)
            {
                Transactions = transactions;
            }
        }

        public class Handler : IRequestHandler<AddCollectionOfTransactions.Command, bool>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                foreach (var transaction in command.Transactions)
                {
                    var transactionExist = await _context.Transactions.FindAsync(transaction.Id);

                    if (transactionExist != null)
                    {
                        transactionExist.Status = transaction.Status;
                    }
                    else
                    {
                        transaction.Id = 0;
                        await _context.Transactions.AddAsync(transaction);
                    }
                    await _context.SaveChangesAsync();
                }
                return true;
            }
        }
    }
}