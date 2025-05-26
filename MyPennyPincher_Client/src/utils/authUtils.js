import { logout } from "../store/slices/userSlice";
import { clearIncomes } from "../store/slices/incomeSlice";
import { clearExpenses } from "../store/slices/expenseSlice";
import { jwtDecode } from "jwt-decode";
import { resetMonth } from "../store/slices/monthSlice";

export function extractTokenExpiryTime(token) {
  const decodedToken = jwtDecode(token);
  return new Date(decodedToken.exp * 1000);
}

export function logoutUser(dispatch, navigate, location) {
  dispatch(logout());
  dispatch(clearIncomes());
  dispatch(clearExpenses());
  dispatch(resetMonth());

  if (location.pathname === "/dashboard") {
    navigate("/login");
  }
}
