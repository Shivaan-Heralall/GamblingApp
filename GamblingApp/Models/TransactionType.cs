using System;
using System.Collections.Generic;

namespace GamblingApp.Models;

public partial class TransactionType
{
    public short TransactionTypeId { get; set; }

    public string TransactionTypeName { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
