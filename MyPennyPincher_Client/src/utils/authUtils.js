import { login, logout, setExpiryTime } from "../store/slices/userSlice";

import { jwtDecode } from "jwt-decode";
import { resetMonth } from "../store/slices/monthSlice";
import { Logout } from "../services/authService";
import { resetMonthRange } from "../store/slices/monthRangeSlice";

export function extractTokenExpiryTime(token) {
  const decodedToken = jwtDecode(token);
  return new Date(decodedToken.exp * 1000);
}

export function loginUser(dispatch, userData) {
  const tokenExpiryTime = extractTokenExpiryTime(userData.token);

  dispatch(setExpiryTime(tokenExpiryTime.toISOString()));

  dispatch(login(userData));
}

export async function logoutUser(dispatch, navigate, location, userId) {
  const response = await Logout(userId);

  if (response != 204) {
    return;
  }

  dispatch(logout());

  dispatch(resetMonth());

  dispatch(resetMonthRange());

  if (location.pathname === "/dashboard") {
    navigate("/login");
  }
}

export function isValidPassword(password) {
  const hasUpperCase = /[A-Z]/.test(password);
  const hasLowerCase = /[a-z]/.test(password);
  const hasDigit = /\d/.test(password);
  const hasSpecialChar = /[\W_]/.test(password);
  const isLongEnough = password.length >= 8;

  return (
    hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar && isLongEnough
  );
}
