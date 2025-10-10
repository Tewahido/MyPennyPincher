import { useQuery } from "@tanstack/react-query";
import { GetUserExpensesForPeriod } from "../services/expenseService.js";

export default function useFetchUserExpenses(
  periodStart,
  periodEnd,
  limit,
  offset,
  yearForTransactions,
  token
) {
  const { data: periodExpenseData, isLoading: periodExpensesLoading } =
    useQuery({
      queryKey: [`Expenses`, periodStart, periodEnd, limit, offset, token],
      queryFn: () =>
        GetUserExpensesForPeriod(token, periodStart, periodEnd, limit, offset),
      enabled: !!token,
    });

  const expensesForPeriod = periodExpenseData ? periodExpenseData.data : [];
  const expensesForPeriodCount = periodExpenseData
    ? periodExpenseData.count
    : 0;

  const maxInt = 2147483647;

  const { data: yearExpenseData, isLoading: yearExpensesLoading } = useQuery({
    queryKey: [
      `Expenses`,
      `${yearForTransactions}-01-01`,
      `${yearForTransactions}-12-31`,
      token,
    ],
    queryFn: () =>
      GetUserExpensesForPeriod(
        token,
        `${yearForTransactions}-01-01`,
        `${yearForTransactions}-12-31`,
        maxInt,
        0
      ),
    enabled: !!token,
  });

  const yearExpenses = yearExpenseData ? yearExpenseData.data : [];

  const loading = periodExpensesLoading || yearExpensesLoading;

  return { expensesForPeriod, yearExpenses, expensesForPeriodCount, loading };
}
