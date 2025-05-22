import { logout } from "../store/slices/userSlice";
import { clearIncomes } from "../store/slices/incomeSlice";
import { clearExpenses } from "../store/slices/expenseSlice";
import { resetMonth } from "../store/slices/monthSlice";

export function logoutUser(dispatch, navigate) {
  dispatch(logout());
  dispatch(clearIncomes());
  dispatch(clearExpenses());
  dispatch(resetMonth());
  navigate("/login");
}
export function getMonthTransactions(transactions, month) {
  const [currentYear, currentMonth] = month.split("-");

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

export function getYearlyTransactions(transactions, month) {
  const [currentYear, currentMonth] = month.split("-");

  return transactions
    ? transactions.filter((transaction) => {
        const transactionYear = new Date(transaction.date).getFullYear();

        return transactionYear == currentYear;
      })
    : null;
}
