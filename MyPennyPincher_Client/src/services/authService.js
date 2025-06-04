import { BASE_URL } from "../config/apiConfig.js";

export const SignUp = async (fullName, email, password) => {
  const response = await fetch(`${BASE_URL}/auth/register`, {
    method: "POST",
    headers: {
      "Content-type": "application/json",
    },
    body: JSON.stringify({ email, fullName, password }),
  });

  if (!response.ok) {
    const responseData = await response.json();
    console.error("Error:", responseData.message);
  }

  const statusCode = await response.status;

  return statusCode;
};

export const Login = async (email, password) => {
  const response = await fetch(`${BASE_URL}/auth/login`, {
    method: "POST",
    credentials: "include",
    headers: {
      "Content-type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });

  if (!response.ok) {
    const error = await response.json();
    console.error("Error:", error.message);
  }

  return response;
};

export const Logout = async (userId) => {
  const response = await fetch(`${BASE_URL}/auth/logout`, {
    method: "POST",
    headers: {
      "Content-type": "application/json",
    },
    body: JSON.stringify(userId),
  });

  if (!response.ok) {
    const error = await response.json();
    console.error("Error:", error.message);
  }

  const statusCode = await response.status;

  return statusCode;
};

export const Refresh = async (userId) => {
  const response = await fetch(`${BASE_URL}/auth/refresh`, {
    method: "POST",
    credentials: "include",
    headers: {
      "Content-type": "application/json",
    },
    body: JSON.stringify(userId),
  });

  return response;
};
