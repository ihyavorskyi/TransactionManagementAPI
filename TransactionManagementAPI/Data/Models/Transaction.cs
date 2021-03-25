using System.ComponentModel.DataAnnotations.Schema;
using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Data
{
    [Table("Transactions")]
    public class Transaction
    {
        [Column("TransactionId")]
        public int Id { get; set; }

        public Status Status { get; set; }
        public Type Type { get; set; }
        public string ClientName { get; set; }
        public string Amount { get; set; }
    }
}