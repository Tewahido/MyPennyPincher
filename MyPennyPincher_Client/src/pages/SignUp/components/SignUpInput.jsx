import ErrorMessage from "../../../components/ErrorMessage";

export default function SignUpInput({ isDataValid, errorMessage, ...props }) {
  return (
    <div className="w-full">
      <input
        className={`w-full outline-2 px-5 py-3 rounded-md text-green-600 ${
          isDataValid ? "outline-gray-500" : "outline-red-400"
        } transition-all duration-50 hover:outline-4 focus:text-green-500`}
        {...props}
      />
      <ErrorMessage text={isDataValid ? "" : errorMessage} />
    </div>
  );
}
