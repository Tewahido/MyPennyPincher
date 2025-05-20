import { BASE_URL } from "../config/config";

export const GetUserIncomes = async (data, token) => {
  const response = await fetch(`${BASE_URL}/Income/getIncomes`, {
    method: "POST",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-type": "application/json",
    },
    body: JSON.stringify(data),
  });

  const incomes = response?.json();

  return incomes;
};

export const AddIncome = async (data, token) => {
  const response = await fetch(`${BASE_URL}/Income/addIncome`, {
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

export const EditIncome = async (data, token) => {
  console.log(data);
  const response = await fetch(`${BASE_URL}/Income`, {
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

export const DeleteIncome = async (data, token) => {
  console.log(data);
  const response = await fetch(`${BASE_URL}/Income`, {
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
