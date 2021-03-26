using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;

namespace TransactionManagementAPI.Features.Query.TransactionsCRUD
{
    public class GeClientByName
    {
        public class Query : IRequest<IEnumerable<string>>
        {
            public string Name { get; set; }

            public Query(string name)
            {
                Name = name;
            }
        }

        public class Handler : IRequestHandler<GeClientByName.Query, IEnumerable<string>>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Transactions.Where(tr => tr.ClientName.Contains(request.Name)).Select(tr => tr.ClientName).ToListAsync();
            }
        }
    }
}