import { BASE_URL } from "../config/config.js";

export const SignUp = async (fullName, email, password) => {
  const response = await fetch(`${BASE_URL}/Auth/register`, {
    method: "POST",
    headers: {
      "Content-type": "application/json",
    },
    body: JSON.stringify({ email, fullName, password }),
  });

  if (!response.ok) {
    const error = await response.text();
    console.error("Error:", error);
    console.log(JSON.stringify(email, fullName, password));
  }

  const statusCode = await response.status;

  return statusCode;
};

export const Login = async (email, password) => {
  const response = await fetch(`${BASE_URL}/Auth/login`, {
    method: "POST",
    headers: {
      "Content-type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });

  if (!response.ok) {
    const error = await response.text();
    console.error("Error:", error);
    return null;
  }

  const data = await response.json();
  return data;
};
