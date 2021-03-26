using System;

namespace TransactionManagementAPI.Data.Enums
{
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