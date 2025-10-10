import { BASE_URL } from "../config/apiConfig.js";

export const GetUserIncomesForPeriod = async (
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

  const response = await fetch(`${BASE_URL}/Income?${queryString}`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-type": "application/json",
    },
  });

  if (response.status === 204) {
    return { data: [], count: 0 };
  }

  if (!response.ok) {
    const error = await response.json();
    console.error("Error:", error.message);
  }

  return response.json();
};

export const AddIncome = async (data, token) => {
  const response = await fetch(`${BASE_URL}/Income`, {
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

  if (!response.ok) {
    const error = await response.json();
    console.error("Error:", error.message);
  }

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
