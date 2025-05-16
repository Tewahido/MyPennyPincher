using System;
using System.Collections.Generic;

namespace MyPennyPincher_API.Models;

public partial class Income
{
    public int IncomeId { get; set; }

    public string Source { get; set; } = null!;

    public int Amount { get; set; }

    public DateOnly Date { get; set; }

    public bool Monthly { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
