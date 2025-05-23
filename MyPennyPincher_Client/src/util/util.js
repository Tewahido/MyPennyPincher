import { logout } from "../store/slices/userSlice";
import { clearIncomes } from "../store/slices/incomeSlice";
import { clearExpenses } from "../store/slices/expenseSlice";
import { resetMonth } from "../store/slices/monthSlice";
import { AddIncome } from "../services/incomeService";
import { AddExpense } from "../services/expenseService";

export function logoutUser(dispatch, navigate) {
  dispatch(logout());
  dispatch(clearIncomes());
  dispatch(clearExpenses());
  dispatch(resetMonth());
  navigate("/login");
}

export function getMonthTransactions(transactions, currentYear, currentMonth) {
  return transactions
    ? transactions.filter((transaction) => {
        const transactionMonth = new Date(transaction.date).getMonth() + 1;
        const trransactionYear = new Date(transaction.date).getFullYear();

        return (
          transactionMonth == currentMonth && trransactionYear == currentYear
        );
      })
    : null;
}

export function getYearlyTransactions(transactions, currentYear) {
  return transactions
    ? transactions.filter((transaction) => {
        const transactionYear = new Date(transaction.date).getFullYear();

        return transactionYear == currentYear;
      })
    : null;
}

export function formatDate(date) {
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
}

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
