using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamblingApp.Models;

public partial class Client
{
    public int ClientId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; } = null!;

  
    public string Surname { get; set; } = null!;
    [Required(ErrorMessage = "Surname is required")]
    [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters")]

    public decimal ClientBalance { get; set; }
    [Required(ErrorMessage = "Client Balance is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative")]

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    //public List<Transaction> Transactions { get; set; } = new List<Transaction>();

}
