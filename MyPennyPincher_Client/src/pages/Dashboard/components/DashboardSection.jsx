import {
  Chart as ChartJS,
  ArcElement,
  Tooltip,
  Legend,
  RadialLinearScale,
  BarElement,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
} from "chart.js";
import { Doughnut, Bar, Line } from "react-chartjs-2";
import TotalDisplay from "./TotalDisplay";

ChartJS.register(
  ArcElement,
  Tooltip,
  Legend,
  RadialLinearScale,
  BarElement,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title
);

const barOptions = {
  responsive: true,
  layout: {
    padding: 5,
  },
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

const donutOptions = {
  responsive: true,
  cutout: "40%",
  layout: {
    padding: 5,
  },
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

const lineOptions = {
  responsive: true,
  layout: {
    padding: 5,
  },
  plugins: {
    legend: {
      display: false,
    },
    title: {
      display: false,
    },
  },
};

let donutData;

let barData;

let lineData;

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
        data: expenses && expenses,
        backgroundColor: "rgba(185, 28, 28, 1)",
        borderRadius: 5,
      },
    ],
  };
}

function setLineChartData(labels, incomes, expenses) {
  let netIncomes = [];

  for (let i = 0; i < incomes.length; i++) {
    netIncomes.push(incomes[i] - expenses[i]);
  }

  lineData = {
    labels: labels,
    datasets: [
      {
        label: "Net Income",
        data: netIncomes,
        borderColor: "rgba(0, 77, 64, 1)",
        backgroundColor: "rgba(0, 77, 64, 0.5)",
        tension: 0.001,
      },
    ],
  };
}

function setDonutChartData(income, expenses) {
  donutData = {
    labels: ["Income", "Expenses"],
    datasets: [
      {
        label: "Monthly Transactions",
        data: [income, expenses],
        backgroundColor: ["rgba(0, 77, 64, 1)", "rgba(185, 28, 28, 1)"],
        borderWidth: 3,
      },
    ],
  };
}

function getTransactionsTotal(transactions) {
  return transactions
    ? transactions.reduce((sum, transaction) => sum + +transaction.amount, 0)
    : 0;
}

function getMonthlyTotals(transactions) {
  if (transactions) {
    let monthTotals = [];

    monthNames.forEach((month, index) => {
      const monthTotal = { month: month, amount: 0 };

      transactions.forEach((transaction) => {
        const date = new Date(transaction.date);
        const monthName = monthNames[date.getMonth()];

        if (monthName === month) {
          monthTotal.amount += transaction.amount;
        }
      });

      monthTotals.push(monthTotal);
    });

    return monthTotals.map((monthTotal) => monthTotal.amount);
  }

  return 0;
}

export default function DashboardSection({
  yearlyTotals,
  currentMonthIncomes,
  currentMonthExpenses,
}) {
  const totalIncome = getTransactionsTotal(currentMonthIncomes);

  const totalExpenses = getTransactionsTotal(currentMonthExpenses);
  let months = [];

  if (yearlyTotals.incomes) {
    yearlyTotals.incomes.map((income) => {
      const date = new Date(income.date);

      if (!months.includes(monthNames[date.getMonth()])) {
        months.push(monthNames[date.getMonth()]);
      }
    });
  }
  const monthlyIncomeAmounts = getMonthlyTotals(yearlyTotals.incomes);

  const monthlyExpenseAmounts = getMonthlyTotals(yearlyTotals.expenses);

  setBarGraphData(monthNames, monthlyIncomeAmounts, monthlyExpenseAmounts);

  setLineChartData(monthNames, monthlyIncomeAmounts, monthlyExpenseAmounts);

  setDonutChartData(totalIncome, totalExpenses);

  const netIncome = totalIncome - totalExpenses;

  const netColour = netIncome > 0 ? "text-green-700" : "text-red-700";

  return (
    <div className="flex flex-col bg-green-100 items-center 2xl:flex-row justify-evenly w-full h-full 2xl:h-150 overflow-hidden ">
      <div className=" hidden 2xl:flex flex-col items-center h-full bg-white m-1 rounded-xl w-[60%] px-15 pb-15">
        <h1 className=" font-bold text-3xl xl:text-4xl black my-10  mx-auto text-center">
          Current month spending
        </h1>
        <div className="flex justify-center h-full xl:h-[80%] w-full">
          <Doughnut data={donutData} options={donutOptions} />
        </div>
      </div>
      <div className="flex flex-col gap-2 h-full w-full">
        <div className="flex flex-col items-center lg:justify-between md:flex-row gap-2 xl:items-start xl:flex-row lg:h-[50%] mx-1 ">
          <TotalDisplay
            title="Income"
            total={totalIncome}
            textcolour="text-green-700/85"
          />
          <TotalDisplay
            title="Expenses"
            total={totalExpenses}
            textcolour="text-red-700/85"
          />
          <TotalDisplay
            title="Net Income"
            total={netIncome}
            textcolour={netColour}
          />
        </div>
        <div className="flex flex-col justify-between items-center md:flex-row h-full w-full">
          <div className=" flex flex-col h-full md:w-[50%] m-1 bg-white rounded-xl gap-10 p-5">
            <h1 className="text-center text-black text-3xl xl:text-4xl font-bold ">
              Monthly spending
            </h1>
            <Bar data={barData} options={barOptions} />
          </div>
          <div className=" flex flex-col h-full  md:w-[50%] m-1 bg-white rounded-xl gap-10 p-5">
            <h1 className="text-center text-black text-3xl xl:text-4xl font-bold ">
              Monthly Net Income
            </h1>
            <Line options={lineOptions} data={lineData} />
          </div>
        </div>
      </div>
    </div>
  );
}
