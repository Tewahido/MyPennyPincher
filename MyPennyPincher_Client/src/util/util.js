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
