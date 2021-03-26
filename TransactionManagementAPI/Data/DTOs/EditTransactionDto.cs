using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Data.DTOs
{
    public class EditTransactionDto
    {
        public int Id { get; set; }
        public TransactionStatus Status { get; set; }
    }
}