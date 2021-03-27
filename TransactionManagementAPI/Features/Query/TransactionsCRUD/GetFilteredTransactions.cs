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
    /// <summary>
    /// Class for getting filtered transactions
    /// </summary>
    public class GetFilteredTransactions
    {
        public class Query : IRequest<IEnumerable<Transaction>>
        {
            /// <summary>
            /// Filters flags for getting transactions
            /// </summary>
            public TransactionFilters Filters { get; set; }

            public Query(int filters)
            {
                Filters = (TransactionFilters)filters;
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
                // Checking whether filtering flags are set
                var hasTypeFlag = request.Filters.HasFlag(TransactionFilters.Withdrawal) || request.Filters.HasFlag(TransactionFilters.Refill);
                var hasStatusFlag = request.Filters.HasFlag(TransactionFilters.Pending) || request.Filters.HasFlag(TransactionFilters.Completed) || request.Filters.HasFlag(TransactionFilters.Cancelled);

                // If filtering flags not set return all transactions
                if (!hasTypeFlag && !hasStatusFlag)
                {
                    return await _context.Transactions.ToListAsync();
                }

                // If have filtering flags for type and status
                if (hasTypeFlag && hasStatusFlag)
                {
                    return await _context.Transactions
                                              .Where(tr => request.Filters.HasFlag((TransactionFilters)tr.Type)
                                              && request.Filters.HasFlag((TransactionFilters)tr.Status)).ToListAsync();
                }
                // If have filtering flags only for type
                else if (hasTypeFlag)
                {
                    return await _context.Transactions
                                            .Where(tr => request.Filters.HasFlag((TransactionFilters)tr.Type)).ToListAsync();
                }
                // If have filtering flags only for status
                else
                {
                    return await _context.Transactions
                        .Where(tr => request.Filters.HasFlag((TransactionFilters)tr.Status)).ToListAsync();
                }
            }
        }
    }
}