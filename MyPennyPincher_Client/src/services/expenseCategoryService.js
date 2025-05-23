import { BASE_URL } from "../config/config.js";

export const GetExpenseCategories = async (token) => {
  const response = await fetch(`${BASE_URL}/ExpenseCategory`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-type": "application/json",
    },
  });

  return response;
};
