import { useState } from "react";
import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { Login } from "../../../services/authService.js";
import LoginInput from "./LoginInput.jsx";
import ErrorMessage from "../../../components/ErrorMessage.jsx";
import { loginUser } from "../../../utils/authUtils.js";

export default function LoginForm() {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [errorMessage, setErrorMessage] = useState();

  const [loginFailed, setLoginFailed] = useState(false);

  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const handleChange = (event) => {
    const { name, value } = event.target;

    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  async function handleSubmit(event) {
    event.preventDefault();
    
    const response = await Login(formData.email, formData.password);

    if (response.status != 200) {
      setLoginFailed(true);
      setErrorMessage(response.message);
      
      return;
    }

    const loggedInUser = await response.json();

    loginUser(dispatch, loggedInUser);
    navigate("/dashboard");
  }

  return (
    <div className="flex flex-col justify-center items-center m-10 ">
      <form onSubmit={handleSubmit} className="flex flex-col gap-3">
        <LoginInput
          type="text"
          name="email"
          placeholder="Email"
          value={formData.email}
          onChange={handleChange}
          loginFailed={loginFailed}
        />
        <LoginInput
          type="password"
          name="password"
          placeholder="Password"
          value={formData.password}
          onChange={handleChange}
          loginFailed={loginFailed}
        />
        <ErrorMessage text={loginFailed ? errorMessage : ""} />
        <div className="w-full flex flex-col gap-3">
          <div className="w-full flex justify-between">
            <p className="text-sm">Don't have an account?</p>
            <Link
              to={"/signup"}
              className="text-green-700 text-sm underline transition-all duration-100 hover:underline-offset-2 hover:text-green-600"
            >
              Sign Up
            </Link>
          </div>
          <div className="w-full flex justify-end">
            <button
              type="submit"
              className="bg-green-600 w-25 h-10 text-white text-xl font-semibold rounded-lg cursor-pointer transition-all duration-100 hover:bg-green-700"
            >
              Login
            </button>
          </div>
        </div>
      </form>
    </div>
  );
}
