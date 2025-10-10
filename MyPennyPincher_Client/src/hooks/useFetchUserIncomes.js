import { useQuery } from "@tanstack/react-query";
import { GetUserIncomesForPeriod } from "../services/incomeService.js";

export default function useFetchUserIncomes(
  periodStart,
  periodEnd,
  limit,
  offset,
  yearForTransactions,
  token
) {
  const { data: periodIncomeData, isLoading: periodIncomesLoading } = useQuery({
    queryKey: [`Incomes`, periodStart, periodEnd, limit, offset, token],
    queryFn: () =>
      GetUserIncomesForPeriod(token, periodStart, periodEnd, limit, offset),
    enabled: !!token,
  });

  const incomesForPeriod = periodIncomeData ? periodIncomeData.data : [];
  const incomesForPeriodCount = periodIncomeData ? periodIncomeData.count : 0;

  const maxInt = 2147483647;

  const { data: yearIncomeData, isLoading: yearIncomesLoading } = useQuery({
    queryKey: [
      `Incomes`,
      `${yearForTransactions}-01-01`,
      `${yearForTransactions}-12-31`,
      token,
    ],
    queryFn: () =>
      GetUserIncomesForPeriod(
        token,
        `${yearForTransactions}-01-01`,
        `${yearForTransactions}-12-31`,
        maxInt,
        0
      ),
    enabled: !!token,
  });

  const yearIncomes = yearIncomeData ? yearIncomeData.data : [];

  const loading = periodIncomesLoading || yearIncomesLoading;

  return { incomesForPeriod, yearIncomes, incomesForPeriodCount, loading };
}
