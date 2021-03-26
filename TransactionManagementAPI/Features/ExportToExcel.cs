using MediatR;
using OfficeOpenXml;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.DTOs;
using TransactionManagementAPI.Features.Query.TransactionsCRUD;

namespace TransactionManagementAPI.Features
{
    public class ExportToExcel
    {
        public class Command : IRequest<bool>
        {
            public ExcelExportOptions Options { get; set; }

            public Command(ExcelExportOptions options)
            {
                Options = options;
            }
        }

        public class Handler : IRequestHandler<ExportToExcel.Command, bool>
        {
            private readonly AppDbContext _context;
            private readonly IMediator _mediator;

            public Handler(AppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                var query = new GetFilteredTransactions.Query(command.Options.TransactionFilters);
                var transactions = await _mediator.Send(query);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(command.Options.FileName + ".xlsx")))
                {
                    ExcelWorksheet excelWorksheet = package.Workbook.Worksheets.Add("Transactions");
                    excelWorksheet.Cells.LoadFromCollection<Transaction>(transactions, true);
                    package.Save();
                }
                return true;
            }
        }
    }
}