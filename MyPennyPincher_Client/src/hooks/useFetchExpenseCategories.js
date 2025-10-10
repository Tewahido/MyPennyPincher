import { useQuery } from "@tanstack/react-query";
import { GetExpenseCategories } from "../services/expenseCategoryService.js";

export default function useFetchExpenseCategories(token) {
  const { data: expenseCategories } = useQuery({
    queryKey: ["ExpenseCategories"],
    queryFn: () => GetExpenseCategories(token),
    enabled: !!token,
  });

  return { expenseCategories };
}
