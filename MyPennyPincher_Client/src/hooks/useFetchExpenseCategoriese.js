import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { GetExpenseCategories } from "../services/expenseCategoryService";
import { setExpenseCategories } from "../store/slices/expenseSlice";

export function useFetchExpenseCategories() {
  const dispatch = useDispatch();
  const token = useSelector((state) => state.user.user.token);

  useEffect(() => {
    async function fetchExpenseCategories() {
      const response = await GetExpenseCategories(token);
      const expenseCategories = await response.json();

      const categoryNames = expenseCategories.map((category) => category.name);

      dispatch(setExpenseCategories(categoryNames));
    }

    fetchExpenseCategories();
  }, [token]);
}
