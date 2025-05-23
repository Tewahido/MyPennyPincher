import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { GetUserExpenses } from "../services/expenseService";
import { setExpenses } from "../store/slices/expenseSlice";
import { GetUserIncomes } from "../services/incomeService";
import { setIncomes } from "../store/slices/incomeSlice";

export function useFetchUserTransactions() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const reloadExpenses = useSelector((state) => state.expense.reloadExpenses);
  const reloadIncomes = useSelector((state) => state.income.reloadIncomes);
  const userLoggedIn = useSelector((state) => state.user.loggedIn);
  const user = useSelector((state) => state.user.user);

  useEffect(() => {
    if (!userLoggedIn) {
      navigate("/login");
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
    console.log();
  }, [reloadIncomes, reloadExpenses]);
}
