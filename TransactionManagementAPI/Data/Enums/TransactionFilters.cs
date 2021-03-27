using System;

namespace TransactionManagementAPI.Data.Enums
{
    /// <summary>
    /// Enumeration of transaction filters (created based on TransactionStatus and TransactionType)
    /// </summary>
    [Flags]
    public enum TransactionFilters
    {
        Pending = 1,
        Completed = 2,
        Cancelled = 4,
        Withdrawal = 8,
        Refill = 16
    }
}