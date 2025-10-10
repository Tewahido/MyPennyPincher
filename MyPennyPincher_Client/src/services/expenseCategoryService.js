import { BASE_URL } from "../config/apiConfig.js";

export const GetExpenseCategories = async (token) => {
  const response = await fetch(`${BASE_URL}/ExpenseCategory`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-type": "application/json",
    },
  });

  if (!response.ok) {
    const text = await response.text();
    throw new Error(
      `Error ${response.status}: ${text || "Failed to fetch categories"}`
    );
  }

  return response.json();
};
