using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamblingApp.Models;

public partial class Transaction
{
    public long TransactionId { get; set; }

    public decimal Amount { get; set; }
    [Required(ErrorMessage = "Amount is required")]
    [Range(-999999.99, 999999.99, ErrorMessage = "Amount must be between -999999.99 and 999999.99")]

    public short TransactionTypeId { get; set; }
    [Required(ErrorMessage = "Transaction Type is required")]
    public int ClientId { get; set; }
    [Required(ErrorMessage = "Client is required")]
    public string? Comment { get; set; }
    [StringLength(100, ErrorMessage = "Comment cannot exceed 100 characters")]
    public virtual Client Client { get; set; } = null!;

    public virtual TransactionType TransactionType { get; set; } = null!;
}
