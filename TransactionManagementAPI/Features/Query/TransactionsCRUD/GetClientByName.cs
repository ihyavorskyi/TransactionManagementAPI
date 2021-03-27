using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;

namespace TransactionManagementAPI.Features.Query.TransactionsCRUD
{
    /// <summary>
    /// Class for searching clients by name
    /// </summary>
    public class GetClientByName
    {
        public class Query : IRequest<IEnumerable<string>>
        {
            /// <summary>
            /// Client name for searching
            /// </summary>
            public string Name { get; set; }

            public Query(string name)
            {
                Name = name;
            }
        }

        public class Handler : IRequestHandler<GetClientByName.Query, IEnumerable<string>>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                // Returns a collection of clients whose names contain the search
                return await _context.Transactions.Where(tr => tr.ClientName.Contains(request.Name)).Select(tr => tr.ClientName).ToListAsync();
            }
        }
    }
}