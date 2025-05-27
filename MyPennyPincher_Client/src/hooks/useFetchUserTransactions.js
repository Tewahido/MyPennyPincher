import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { GetUserExpenses } from "../services/expenseService";
import { setExpenses } from "../store/slices/expenseSlice";
import { GetUserIncomes } from "../services/incomeService";
import { setIncomes } from "../store/slices/incomeSlice";
import { setLoading } from "../store/slices/loadingSlice";
import { logoutUser } from "../utils/authUtils";
import { GetExpenseCategories } from "../services/expenseCategoryService";
import { setExpenseCategories } from "../store/slices/expenseSlice";

export function useFetchUserTransactions() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const reloadExpenses = useSelector((state) => state.expense.reloadExpenses);
  const reloadIncomes = useSelector((state) => state.income.reloadIncomes);
  const userLoggedIn = useSelector((state) => state.user.loggedIn);
  const user = useSelector((state) => state.user.user);

  useEffect(() => {
    if (!user) {
      navigate("/login");
      return;
    }

    async function fetchIncomes() {
      const userIncomes = await GetUserIncomes(user.token);
      if (userIncomes?.status === 401) {
        logoutUser(dispatch, navigate);
      } else {
        const data = await userIncomes.json();
        dispatch(setIncomes(data));
      }
    }

    fetchIncomes();

    async function fetchExpenses() {
      const userExpenses = await GetUserExpenses(user.token);
      if (userExpenses?.status === 401) {
        logoutUser(dispatch, navigate);
      } else {
        const data = await userExpenses.json();
        dispatch(setExpenses(data));
      }
    }

    fetchExpenses();

    async function fetchExpenseCategories() {
      const response = await GetExpenseCategories(user.token);
      const expenseCategories = await response.json();

      const categoryNames = expenseCategories.map((category) => category.name);

      dispatch(setExpenseCategories(categoryNames));
    }

    fetchExpenseCategories();

    setTimeout(() => dispatch(setLoading(false)), 2000);
  }, [reloadIncomes, reloadExpenses, user, userLoggedIn]);
}
