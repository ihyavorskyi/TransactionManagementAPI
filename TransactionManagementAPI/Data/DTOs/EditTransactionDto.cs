using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Data.DTOs
{
    public class EditTransactionDto
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}