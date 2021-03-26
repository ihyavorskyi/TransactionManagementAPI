using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Features.Query.WorkWithCsv;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    public class AddCollectionOfTransactions
    {
        public class Command : IRequest<bool>
        {
            public string FileName { get; set; }

            public Command(string fileName)
            {
                FileName = fileName;
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
                var transactions = CsvHelper.ParseCsvTransaction(command.FileName);

                foreach (var transaction in transactions)
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
                }
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}