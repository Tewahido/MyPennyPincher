import { Link, useNavigate } from "react-router-dom";
import { SignUp } from "../../../services/apiService";
import { useState } from "react";
import ErrorMessage from "./ErrorMessage";

const emailDomain = [".com", ".co.za", ".org"];

function isValidPassword(password) {
  const hasUpperCase = /[A-Z]/.test(password);
  const hasLowerCase = /[a-z]/.test(password);
  const hasDigit = /\d/.test(password);
  const hasSpecialChar = /[\W_]/.test(password);
  const isLongEnough = password.length >= 8;

  return (
    hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar && isLongEnough
  );
}

export default function SignUpForm() {
  const [isDataValid, setIsDataValid] = useState({
    email: true,
    fullName: true,
    password: true,
    confirmPassword: true,
  });

  const [formData, setFormData] = useState({
    fullName: "",
    email: "",
    password: "",
    confirmPassword: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const navigate = useNavigate();

  async function handleSubmit(event) {
    event.preventDefault();

    if (!isFormValid(formData)) {
      return;
    }

    const signUpStatus = await SignUp(
      formData.fullName,
      formData.email,
      formData.password
    );

    console.log(formData);

    if (signUpStatus === 200) {
      navigate("/login");
    }
  }

  function isFormValid(formData) {
    const emailIsValid =
      emailDomain.some((requiredCharacter) =>
        formData.email.includes(requiredCharacter)
      ) && formData.email.includes("@");

    const nameNotNull = formData.fullName.length > 0;

    const passwordIsValid = isValidPassword(formData.password);

    const passwordsMatch = formData.password === formData.confirmPassword;

    setIsDataValid({
      email: emailIsValid,
      fullName: nameNotNull,
      password: passwordIsValid,
      confirmPassword: passwordsMatch,
    });

    return emailIsValid && nameNotNull && passwordIsValid && passwordsMatch;
  }

  return (
    <div className="flex flex-col justify-center items-center m-10 ">
      <form
        onSubmit={handleSubmit}
        className="flex flex-col items-center gap-3"
      >
        <div>
          <input
            type="text"
            className={`w-full outline-2 px-5 py-3 rounded-md text-green-600 ${
              isDataValid.email ? "outline-gray-500" : "outline-red-400"
            } transition-all duration-50 hover:outline-4`}
            name="email"
            placeholder="Email"
            value={formData.email}
            onChange={handleChange}
          />
          <ErrorMessage
            text={
              isDataValid.email ? "" : "Please enter a valid email address."
            }
          />
        </div>
        <div>
          <input
            type="text"
            className={`w-full outline-2 px-5 py-3 rounded-md text-green-600 ${
              isDataValid.fullName ? "outline-gray-500" : "outline-red-400"
            } transition-all duration-50 hover:outline-4`}
            name="fullName"
            placeholder="Full Name"
            value={formData.fullName}
            onChange={handleChange}
          />
          <ErrorMessage
            text={isDataValid.fullName ? "" : "Please enter your full name."}
          />
        </div>
        <div>
          <input
            type="password"
            className={`w-full outline-2 px-5 py-3 rounded-md text-green-600 ${
              isDataValid.password ? "outline-gray-500" : "outline-red-400"
            } transition-all duration-50 hover:outline-4`}
            name="password"
            placeholder="Password"
            value={formData.password}
            onChange={handleChange}
          />
          <ErrorMessage
            text={
              isDataValid.password ? "" : "Please enter a stronger password."
            }
          />
        </div>
        <div>
          <input
            type="password"
            className={`w-full outline-2 px-5 py-3 rounded-md text-green-600 ${
              isDataValid.confirmPassword
                ? "outline-gray-500"
                : "outline-red-400"
            } transition-all duration-50 hover:outline-4 `}
            name="confirmPassword"
            placeholder="Confirm Password"
            value={formData.confirmPassword}
            onChange={handleChange}
          />
          <ErrorMessage
            text={isDataValid.confirmPassword ? "" : "Passwords do not match."}
          />
        </div>
        <div className="w-full flex flex-col gap-3">
          <div className="w-full flex justify-between">
            <p className="text-sm">Already have an account?</p>
            <Link
              to={"/login"}
              className="text-sm text-green-700 underline transition-all duration-100 hover:underline-offset-2 hover:text-green-600"
            >
              Login
            </Link>
          </div>
          <div className="w-full flex justify-start">
            <button
              type="submit"
              className="bg-green-600 w-25 h-10 text-white text-xl font-semibold rounded-lg cursor-pointer transition-all duration-100 hover:bg-green-700"
            >
              Sign Up
            </button>
          </div>
          <ErrorMessage
            text={
              isDataValid.password
                ? ""
                : `Password must be 8 characters long and include
                  -An uppercase letter
                  -A lowercase letter
                  -A number
                  -A special character`
            }
          />
        </div>
      </form>
    </div>
  );
}
