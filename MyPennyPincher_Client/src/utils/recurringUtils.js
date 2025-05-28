import { AddIncome } from "../services/incomeService";
import { AddExpense } from "../services/expenseService";
import { formatDate } from "./dateUtils";

export function addMonthlyIncome(income, token) {
  let incomeMonth = new Date(income.date).getMonth() + 1;

  for (incomeMonth; incomeMonth < 12; incomeMonth++) {
    const newDate = new Date(income.date);
    newDate.setMonth(incomeMonth);
    const newIncome = { ...income, date: formatDate(newDate) };
    AddIncome(newIncome, token);
  }
}

export function addRecurringExpense(expense, token) {
  let expenseMonth = new Date(expense.date).getMonth() + 1;

  for (expenseMonth; expenseMonth < 12; expenseMonth++) {
    const newDate = new Date(expense.date);
    newDate.setMonth(expenseMonth);
    const newExpense = { ...expense, date: formatDate(newDate) };
    AddExpense(newExpense, token);
  }
}
