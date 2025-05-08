import SummarySection from "./SummarySection";

export default function Dashboard() {
  return (
    <div className="w-full flex flex-col mt-20">
      <div className="w-[50%] h-50 flex items-end justify-center mb-10 mx-10">
        <h1 className="text-8xl font-extrabold text-green-700"> MySummary</h1>
      </div>
      <div className="w-full h-150">
        <SummarySection />
      </div>
      <hr className="bg-green-700 h-0.5 w-[70%] mx-auto my-5" />
      <div className="p-3">
        <div className="flex flex-col items-center bg-green-700 h-200 rounded-4xl p-10">
          <h1 className="text-6xl font-extrabold text-white">
            All Transactions
          </h1>
        </div>
      </div>
    </div>
  );
}
