using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Features.Query.WorkWithCsv;

namespace TransactionManagementAPI.Features.Commands.TransactionsCRUD
{
    /// <summary>
    /// Class that causes transaction parsing from a Csv file,
    /// and adds them to the database using merge on id transaction
    /// </summary>
    public class AddCollectionOfTransactions
    {
        public class Command : IRequest<string>
        {
            /// <summary>
            /// Csv file name for parsing transaction from him
            /// </summary>
            public string FileName { get; set; }

            public Command(string fileName)
            {
                FileName = fileName;
            }
        }

        public class Handler : IRequestHandler<AddCollectionOfTransactions.Command, string>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(Command command, CancellationToken cancellationToken)
            {
                // Parsing transaction from Csv file
                var transactions = CsvHelper.ParseCsvTransaction(command.FileName);

                foreach (var transaction in transactions)
                {
                    var transactionExist = await _context.Transactions.FindAsync(transaction.Id);

                    // If transaction exist update her status,
                    // else create new transaction in DataBase
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
                return "New transactions added or updated them status";
            }
        }
    }
}