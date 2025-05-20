using System;
using System.Collections.Generic;

namespace MyPennyPincher_API.Models;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public string Description { get; set; } = null!;

    public int Amount { get; set; }

    public DateOnly Date { get; set; }

    public bool Recurring { get; set; }

    public Guid UserId { get; set; }

    public int ExpenseCategoryId { get; set; }

}
