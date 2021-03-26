using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Features.Query.TransactionsCRUD
{
    public class GetFilteredTransactions
    {
        public class Query : IRequest<IEnumerable<Transaction>>
        {
            public TransactionFilters Filters { get; set; }

            public Query(TransactionFilters filters)
            {
                Filters = filters;
            }
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
                var transactions = new List<Transaction>();

                var hasTypeFlag = request.Filters.HasFlag(TransactionFilters.Withdrawal) || request.Filters.HasFlag(TransactionFilters.Refill);
                var hasStatusFlag = request.Filters.HasFlag(TransactionFilters.Pending) || request.Filters.HasFlag(TransactionFilters.Completed) || request.Filters.HasFlag(TransactionFilters.Cancelled);

                if (hasTypeFlag)
                {
                    if (hasStatusFlag)
                    {
                        transactions = await _context.Transactions
                                                .Where(tr => request.Filters.HasFlag((TransactionFilters)tr.Type)
                                                && request.Filters.HasFlag((TransactionFilters)tr.Status))
                                                .ToListAsync();
                    }
                    else
                    {
                        transactions = await _context.Transactions
                                                .Where(tr => request.Filters.HasFlag((TransactionFilters)tr.Type))
                                                .ToListAsync();
                    }
                }
                else
                {
                    transactions = await _context.Transactions
                        .Where(tr => request.Filters.HasFlag((TransactionFilters)tr.Status))
                        .ToListAsync();
                }

                return transactions;
            }
        }
    }
}