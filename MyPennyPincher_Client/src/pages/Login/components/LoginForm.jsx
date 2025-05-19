import { useState } from "react";
import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { Login } from "../../../services/apiService";
import { login } from "../../../store/slices/userSlice.js";

export default function LoginForm() {
  const navigate = useNavigate();

  const dispatch = useDispatch();

  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  async function handleSubmit(event) {
    event.preventDefault();

    const loggedInUser = await Login(formData.email, formData.password);

    console.log(loggedInUser);

    if (loggedInUser) {
      dispatch(login(loggedInUser));
      navigate("/dashboard");
    }
  }

  return (
    <div className="flex flex-col justify-center items-center m-10 ">
      <form onSubmit={handleSubmit} className="flex flex-col gap-5">
        <input
          type="text"
          className="outline-2 px-5 py-3 rounded-md text-green-600 outline-gray-500 transition-all duration-50 hover:outline-4"
          name="email"
          placeholder="Email"
          value={formData.email}
          onChange={handleChange}
        />
        <input
          type="password"
          className="outline-2 px-5 py-3 rounded-md text-green-600 outline-gray-500 transition-all duration-50 hover:outline-4"
          name="password"
          placeholder="Password"
          value={formData.password}
          onChange={handleChange}
        />
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
