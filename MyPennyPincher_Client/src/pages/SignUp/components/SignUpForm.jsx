import { Link, useNavigate } from "react-router-dom";
import { SignUp } from "../../../services/authService.js";
import { isValidElement, useState } from "react";
import ErrorMessage from "../../../components/ErrorMessage.jsx";
import SignUpInput from "./SignUpInput.jsx";
import { isValidPassword } from "../../../utils/authUtils.js";

const emailDomain = [".com", ".co.za", ".org"];

export default function SignUpForm() {
  const [userExists, setUserExists] = useState(false);

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
    setUserExists(false);

    if (!isFormValid(formData)) {
      return;
    }

    const signUpStatus = await SignUp(
      formData.fullName,
      formData.email,
      formData.password
    );

    console.log(formData);

    if (signUpStatus === 409) {
      setUserExists(true);
      setFormData({
        fullName: "",
        email: "",
        password: "",
        confirmPassword: "",
      });
    }

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
        <SignUpInput
          type="text"
          name="email"
          placeholder="Email"
          value={formData.email}
          onChange={handleChange}
          isDataValid={isDataValid.email}
          errorMessage="Please enter a valid email."
        />
        <SignUpInput
          type="text"
          name="fullName"
          placeholder="Full Name"
          value={formData.fullName}
          onChange={handleChange}
          isDataValid={isDataValid.fullName}
          errorMessage="Please enter your name."
        />
        <SignUpInput
          type="password"
          name="password"
          placeholder="Password"
          value={formData.password}
          onChange={handleChange}
          isDataValid={isDataValid.password}
          errorMessage="Please enter a stronger password"
        />
        <SignUpInput
          type="password"
          name="confirmPassword"
          placeholder="Confirm Password"
          value={formData.confirmPassword}
          onChange={handleChange}
          isDataValid={isDataValid.confirmPassword}
          errorMessage="Passwords do not match."
        />
        {userExists && (
          <ErrorMessage text="A user with this email already exists" />
        )}
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
                : `Password must be 8 characters long and include:
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
