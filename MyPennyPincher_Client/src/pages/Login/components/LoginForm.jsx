import { Link } from "react-router-dom";

export default function LoginForm() {
  function handleSubmit(event) {
    event.preventDefault();
  }

  return (
    <div className="flex flex-col justify-center items-center m-10 ">
      <form onSubmit={handleSubmit} className="flex flex-col gap-5">
        <input
          type="text"
          className="outline-2 px-5 py-3 rounded-md text-green-600 outline-gray-500 transition-all duration-50 hover:outline-4"
          name="email"
          placeholder="Email"
        />
        <input
          type="password"
          className="outline-2 px-5 py-3 rounded-md text-green-600 outline-gray-500 transition-all duration-50 hover:outline-4"
          name="password"
          placeholder="Password"
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
