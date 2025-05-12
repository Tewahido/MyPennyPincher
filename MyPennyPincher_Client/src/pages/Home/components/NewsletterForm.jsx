export default function NewsletterForm() {
  return (
    <>
      <h1 className="text-white text-center font-bold text-3xl lg:text-5xl ">
        Sign-Up For Our Newsletter
      </h1>
      <p className=" italic text-white mt-5 text-center">
        Be the first to know about our exciting new features!
      </p>
      <div className="flex flex-col items-start w-100 bg-green-950 rounded-2xl justify-start  p-10 mt-6">
        <div className="flex flex-col items-center gap-4 w-full ">
          <input
            title="userEmail"
            placeholder="Enter your email..."
            type="text"
            className="italic font-semibold text-white border-1 border-white rounded-md p-4 w-[80%] h-10 mb-3"
          />
          <button className="cursor-pointer italic  text-gray-200/85 bg-green-950 border-1 border-gray-300 rounded-lg px-2 py-1  transition-all duration-200 hover:text-white hover:border-white hover:scale-105">
            Sign-up
          </button>
        </div>
      </div>
    </>
  );
}
