import SummarySection from "./components/SummarySection";

export default function Dashboard() {
  return (
    <div className="w-full bg-green-100 flex flex-col mt-10">
      <div className="w-[50%] h-50 flex items-end justify-center mb-10 mx-10">
        <h1 className="text-8xl font-extrabold text-green-700"> MySummary</h1>
      </div>
      <div className="w-full ">
        <SummarySection />
      </div>
      <hr className="bg-green-700 h-0.5 w-[70%] mx-auto my-5" />
      <div className="p-3">
        <div className="flex flex-col items-center bg-white h-200 rounded-4xl p-10">
          <h1 className="text-6xl font-extrabold text-green-700">
            All Transactions
          </h1>
        </div>
      </div>
    </div>
  );
}
