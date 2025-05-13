import {
  Chart as ChartJS,
  ArcElement,
  Tooltip,
  Legend,
  RadialLinearScale,
  BarElement,
  CategoryScale,
  LinearScale,
} from "chart.js";
import { Doughnut, PolarArea, Bar } from "react-chartjs-2";

ChartJS.register(
  ArcElement,
  Tooltip,
  Legend,
  RadialLinearScale,
  BarElement,
  CategoryScale,
  LinearScale
);

const data = {
  labels: ["Rent", "Groceries", "Transport"],
  datasets: [
    {
      label: "Monthly Expendture",
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

const barOptions = {
  responsive: true,
  scales: {
    y: {
      beginAtZero: true,
      ticks: {
        stepSize: 500,
      },
    },
  },
  legend: {
    position: "bottom",
  },
};

const options = {
  responsive: true,
  cutout: "40%",
  plugins: {
    legend: {
      position: "bottom",
      labels: {
        font: {
          size: 16,
        },
      },
    },
  },
};

const monthNames = [
  "January",
  "February",
  "March",
  "April",
  "May",
  "June",
  "July",
  "August",
  "September",
  "October",
  "November",
  "December",
];

let barData;

function setBarGraphData(labels, incomes, expenses) {
  barData = {
    labels: labels,
    datasets: [
      {
        label: "Total Income",
        data: incomes,
        backgroundColor: "rgba(0, 77, 64, 1)",
        borderRadius: 5,
      },
      {
        label: "Total Expenses",
        data: expenses,
        backgroundColor: "rgba(200, 230, 201, 1)",
        borderRadius: 5,
      },
    ],
  };
}

function getTotal(transactions) {
  return transactions.reduce((sum, transaction) => sum + transaction.Amount, 0);
}

function getAmounts(transactions) {
  return transactions.map((transaction) => {
    return transaction.Amount;
  });
}

export default function DashboardSection({
  totals,
  currentMonthIncomes,
  currentMonthExpenses,
}) {
  const totalIncome = getTotal(currentMonthIncomes);

  const totalExpenses = getTotal(currentMonthExpenses);

  const months = totals.incomes.map((income) => {
    const date = new Date(income.Date);
    return monthNames[date.getMonth()];
  });

  const totalIncomeAmounts = getAmounts(totals.incomes);

  const totalExpenseAmounts = getAmounts(totals.expenses);

  setBarGraphData(months, totalIncomeAmounts, totalExpenseAmounts);

  const netIncome = totalIncome - totalExpenses;

  const netColour = netIncome > 0 ? "text-green-700" : "text-red-700";

  return (
    <div className="flex justify-evenly w-full h-150 px-20">
      <div className="flex flex-col items-center h-full bg-white m-1 rounded-xl w-[60%] px-15 pb-15">
        <h1 className=" font-bold text-4xl text-green-700 my-10  mx-auto text-center">
          Current month spending{" "}
        </h1>
        <div className="flex justify-center h-[80%] w-full">
          <Doughnut data={data} options={options} />
        </div>
      </div>
      <div className="flex flex-col gap-2 h-full w-full ">
        <div className="flex lg:h-[50%]">
          <div className="flex flex-col items-center h-full w-[33%] bg-white m-1 rounded-xl p-5">
            <h1 className="text-4xl font-bold  text-center">Total Income</h1>
            <h1 className="text-8xl text-green-700/85 font-semibold">
              R{totalIncome}
            </h1>
          </div>
          <div className="flex flex-col items-center h-full w-[33%] bg-white m-1 rounded-xl p-5">
            <h1 className="text-4xl font-bold  text-center">Total Expenses</h1>
            <h1 className="text-8xl text-red-700/85 font-semibold">
              R{totalExpenses}
            </h1>
          </div>
          <div className="flex flex-col items-center h-full w-[33%] bg-white m-1 rounded-xl p-5">
            <h1 className="text-4xl font-bold  text-center">Net Income</h1>
            <h1 className={`text-8xl ${netColour}/85 font-semibold`}>
              R{netIncome}
            </h1>
          </div>
        </div>
        <div className="flex h-full w-full">
          <div className=" flex flex-col h-full w-[50%] m-1 bg-white rounded-xl gap-10 p-10">
            <h1 className="text-center text-green-700 text-4xl font-bold ">
              Monthly spending
            </h1>
            <Bar data={barData} options={barOptions} />
          </div>
          <div className="h-full w-[50%] m-1 bg-white rounded-xl"></div>
        </div>
      </div>
    </div>
  );
}
