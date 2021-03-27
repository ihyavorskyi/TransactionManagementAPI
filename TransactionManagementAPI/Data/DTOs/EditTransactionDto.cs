using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Data.DTOs
{
    /// <summary>
    /// Model for editing transaction status
    /// </summary>
    public class EditTransactionDto
    {
        public int Id { get; set; }
        public TransactionStatus Status { get; set; }
    }
}