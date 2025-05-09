export default function ContactUsForm() {
  return (
    <>
      {" "}
      <h1 className="text-white text-center font-bold text-5xl ">Contact us</h1>
      <div className="flex flex-col items-start gap-4 w-100  bg-green-950 rounded-2xl  p-10 mt-10">
        <div className="flex flex-col items-center gap-4 w-full">
          <input
            title="userEmail"
            placeholder="Enter your email..."
            type="text"
            className="italic font-semibold text-white border-1 border-white rounded-md p-4 w-[80%] h-10 "
          />
          <textarea
            title="userMessage"
            placeholder="Enter your message..."
            type="text"
            className="  italic font-semibold text-white border-1  border-white rounded-md p-4 w-[80%] h-75 "
          />
          <div className="w-[80%]">
            <button className="cursor-pointer italic  text-gray-200/85 bg-green-950 border-1 border-gray-300 rounded-lg p-2 transition-all duration-200 hover:text-white hover:border-white hover:scale-105">
              Send Message
            </button>
          </div>
        </div>
      </div>
    </>
  );
}
