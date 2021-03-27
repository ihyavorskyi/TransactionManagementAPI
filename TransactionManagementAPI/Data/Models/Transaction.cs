using System.ComponentModel.DataAnnotations.Schema;
using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Data
{
    /// <summary>
    /// Transaction model
    /// </summary>

    [Table("Transactions")]
    public class Transaction
    {
        [Column("TransactionId")]
        public int Id { get; set; }

        public TransactionStatus Status { get; set; }
        public TransactionType Type { get; set; }
        public string ClientName { get; set; }
        public float Amount { get; set; }
    }
}