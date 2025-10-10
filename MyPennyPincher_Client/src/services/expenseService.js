import { BASE_URL } from "../config/apiConfig.js";
import { STATUS_CODES } from "../constants/statusCodes.js";

export const GetUserExpensesForPeriod = async (
  token,
  periodStart,
  periodEnd,
  limit,
  offset
) => {
  const queryString = new URLSearchParams({
    periodStart,
    periodEnd,
    ...(limit && { limit }),
    ...(offset && { offset }),
  }).toString();

  const response = await fetch(`${BASE_URL}/Expense?${queryString}`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-type": "application/json",
    },
  });

  if (response.status === STATUS_CODES.NO_CONTENT) {
    return { data: [], count: 0 };
  }

  if (!response.ok) {
    const error = await response.json();
    console.error("Error:", error.message);
  }

  return response.json();
};

export const AddExpense = async (data, token) => {
  const response = await fetch(`${BASE_URL}/Expense`, {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    const error = await response.json();
    console.error("Error:", error.message);
  }

  const status = response.status;

  return status;
};

export const EditExpense = async (data, token) => {
  const response = await fetch(`${BASE_URL}/Expense`, {
    method: "PUT",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    const error = await response.json();
    console.error("Error:", error.message);
  }

  const status = response.status;

  return status;
};

export const DeleteExpense = async (data, token) => {
  console.log(data);
  const response = await fetch(`${BASE_URL}/Expense`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    const error = await response.json();
    console.error("Error:", error.message);
  }

  const status = response.status;

  return status;
};
