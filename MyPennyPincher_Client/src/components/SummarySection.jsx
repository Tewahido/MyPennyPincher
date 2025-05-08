import { Chart as ChartJS, ArcElement, Tooltip, Legend } from "chart.js";
import { Doughnut } from "react-chartjs-2";

ChartJS.register(ArcElement, Tooltip, Legend);

const data = {
  labels: ["Rent", "Groceries", "Transport"],
  datasets: [
    {
      label: "Amount spent",
      data: [500, 300, 200],
      backgroundColor: [
        "rgba(0, 77, 64, 1)",
        "rgba(56, 142, 60, 1)",
        "rgba(200, 230, 201, 1)",
      ],
      borderWidth: 3,
    },
  ],
};

const options = {
  responsive: true,
  cutout: "40%",
  plugins: {
    legend: {
      position: "bottom",
      labels: {
        font: {
          size: 20,
        },
      },
    },
  },
};

export default function SummarySection() {
  return (
    <div className="flex justify-evenly  h-150 px-20">
      <div className="flex justify-center h-full w-full p-15">
        <Doughnut data={data} options={options} />
      </div>
      <div className="w-full h-full p-15">
        <div className="flex flex-col items-center bg-green-700 h-full w-[60%] mx-auto rounded-2xl shadow-[0px_0px_10px]">
          <h1 className="text-white font-bold text-2xl my-10 w-[50%] mx-auto text-center">
            Highest Spending By Category
          </h1>
          <hr className="bg-white h-0.5 w-[70%]" />
          <div className="flex flex-col h-[60%] justify-center text-white text-2xl font-semibold gap-10 items-center text-center">
            <p>Rent : R500</p>
            <p>Groceries : R300</p>
            <p>Transport : R200</p>
          </div>
        </div>
      </div>
    </div>
  );
}
