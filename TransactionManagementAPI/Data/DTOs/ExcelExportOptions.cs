using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Data.DTOs
{
    public class ExcelExportOptions
    {
        public string FileName { get; set; }
        public TransactionFilters TransactionFilters { get; set; }
    }
}