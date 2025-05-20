export default function LoginInput({ loginFailed, ...props }) {
  return (
    <input
      className={`outline-2 px-5 py-3 rounded-md text-green-600 ${
        loginFailed ? "outline-red-500" : "outline-gray-500"
      } transition-all duration-50 hover:outline-4 focus:text-green-500`}
      {...props}
    />
  );
}
