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
    /// <summary>
    /// Class for exporting filtred transactions to excel file
    /// </summary>
    public class ExportToExcel
    {
        public class Command : IRequest<string>
        {
            /// <summary>
            /// Settings for export
            /// </summary>
            public ExcelExportSettings Settings { get; set; }

            public Command(ExcelExportSettings settings)
            {
                Settings = settings;
            }
        }

        public class Handler : IRequestHandler<ExportToExcel.Command, string>
        {
            private readonly IMediator _mediator;

            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<string> Handle(Command command, CancellationToken cancellationToken)
            {
                // Receiving transactions on the set filters
                var query = new GetFilteredTransactions.Query((int)command.Settings.TransactionFilters);
                var transactions = await _mediator.Send(query);

                // Creating new excel file and writing transactions to him
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo("ExcelFiles\\" + command.Settings.FileName + ".xlsx")))
                {
                    ExcelWorksheet excelWorksheet = package.Workbook.Worksheets.Add("Transactions");
                    excelWorksheet.Cells.LoadFromCollection<Transaction>(transactions, true);
                    package.Save();
                }
                return "Created new file :" + command.Settings.FileName + ".xlsx. You can found them in ExcelFiles folder";
            }
        }
    }
}