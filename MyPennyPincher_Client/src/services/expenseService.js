import { BASE_URL } from "../config/apiConfig.js";

export const GetUserExpenses = async (token) => {
  const response = await fetch(`${BASE_URL}/Expense`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-type": "application/json",
    },
  });

  return response;
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
    console.error();

    const error = await response.text();
    console.error("Error:", error);
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

  const status = response.status;

  return status;
};
