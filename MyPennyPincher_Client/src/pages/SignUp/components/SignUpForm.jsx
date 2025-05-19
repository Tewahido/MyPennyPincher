import { Link, useNavigate } from "react-router-dom";
import { SignUp } from "../../../services/apiService";
import { useState } from "react";

export default function SignUpForm() {
  const [formData, setFormData] = useState({
    fullName: "",
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

  const navigate = useNavigate();

  async function handleSubmit(event) {
    event.preventDefault();

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
          type="text"
          className="outline-2 px-5 py-3 rounded-md text-green-600 outline-gray-500 transition-all duration-50 hover:outline-4"
          name="fullName"
          placeholder="Full Name"
          value={formData.fullName}
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
        <input
          type="password"
          className="outline-2 px-5 py-3 rounded-md text-green-600 outline-gray-500 transition-all duration-50 hover:outline-4"
          name="confirmPassword"
          placeholder="Confirm Password"
        />
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
        </div>
      </form>
    </div>
  );
}
