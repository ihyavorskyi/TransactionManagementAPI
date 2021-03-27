using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Data.DTOs
{
    /// <summary>
    /// Model with setting for export transaction to excel file
    /// </summary>
    public class ExcelExportSettings
    {
        public string FileName { get; set; }
        public TransactionFilters TransactionFilters { get; set; }
    }
}