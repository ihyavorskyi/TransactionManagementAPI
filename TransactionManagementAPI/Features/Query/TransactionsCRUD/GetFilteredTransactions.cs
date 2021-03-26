using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.TransactionFilters;

namespace TransactionManagementAPI.Features.Query.TransactionsCRUD
{
    public class GetFilteredTransactions
    {
        public class Query : IRequest<IEnumerable<Transaction>>
        {
        }

        public class Handler : IRequestHandler<GetFilteredTransactions.Query, IEnumerable<Transaction>>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Transaction>> Handle(Query request, CancellationToken cancellationToken)
            {
                var transactions = await _context.Transactions
                    .Select(tr => new Transaction
                    {
                        Id = tr.Id,
                        Status = tr.Status,
                        Type = tr.Type,
                        ClientName = tr.ClientName,
                        Amount = tr.Amount
                    }).ToListAsync();
                return transactions;
            }
        }
    }
}