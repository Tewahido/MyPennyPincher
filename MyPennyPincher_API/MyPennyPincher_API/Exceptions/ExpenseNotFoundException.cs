﻿namespace MyPennyPincher_API.Exceptions
{
    public class ExpenseNotFoundException :Exception
    {
        public ExpenseNotFoundException(int expenseId): base($"Expense with ID {expenseId} not found."){ }
    }
}
