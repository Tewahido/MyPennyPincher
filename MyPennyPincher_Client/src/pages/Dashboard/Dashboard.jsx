import { useEffect, useState } from "react";
import DashboardSection from "./components/DashboardSection";
import TransactionsSection from "./components/TransactionsSection";
import { useDispatch, useSelector } from "react-redux";
import { setMonth } from "../../store/slices/monthSlice";
import { GetUserIncomes } from "../../services/incomeService";
import { setIncomes } from "../../store/slices/incomeSlice";

const expenseData = [
  {
    expenseId: 1,
    description: "Rent",
    amount: 1200,
    date: "2025-01-01",
    recurring: true,
    categoryId: 1,
    userId: 1,
  },
  {
    expenseId: 2,
    description: "Electricity Bill",
    amount: 100,
    date: "2025-02-05",
    recurring: true,
    categoryId: 2,
    userId: 1,
  },
  {
    expenseId: 3,
    description: "Groceries",
    amount: 450,
    date: "2025-03-10",
    recurring: true,
    categoryId: 3,
    userId: 1,
  },
  {
    expenseId: 4,
    description: "New Phone",
    amount: 900,
    date: "2025-04-15",
    recurring: false,
    categoryId: 4,
    userId: 1,
  },
  {
    expenseId: 5,
    description: "Vacation Trip",
    amount: 1500,
    date: "2025-05-02",
    recurring: false,
    categoryId: 5,
    userId: 1,
  },
  {
    expenseId: 6,
    description: "Internet Subscription",
    amount: 60,
    date: "2025-01-10",
    recurring: true,
    categoryId: 2,
    userId: 1,
  },
  {
    expenseId: 7,
    description: "Gym Membership",
    amount: 50,
    date: "2025-02-12",
    recurring: true,
    categoryId: 6,
    userId: 1,
  },
  {
    expenseId: 8,
    description: "Movie Night",
    amount: 30,
    date: "2025-03-21",
    recurring: false,
    categoryId: 7,
    userId: 1,
  },
  {
    expenseId: 9,
    description: "Dining Out",
    amount: 75,
    date: "2025-04-10",
    recurring: false,
    categoryId: 7,
    userId: 1,
  },
  {
    expenseId: 10,
    description: "Clothing",
    amount: 200,
    date: "2025-05-08",
    recurring: false,
    categoryId: 8,
    userId: 1,
  },
];

export default function Dashboard() {
  const incomeData = useSelector((state) => state.income.incomes);
  const user = useSelector((state) => state.user.user);
  const month = useSelector((state) => state.month.month);

  const dispatch = useDispatch();

  useEffect(() => {
    async function fetchIncomes() {
      const userIncomes = await GetUserIncomes(user.userId, user.token);
      dispatch(setIncomes(userIncomes));
    }
    fetchIncomes();
  }, []);

  const [currentYear, currentMonth] = month.split("-");

  const currentMonthIncomes = incomeData
    ? incomeData.filter((income) => {
        const incomeMonth = new Date(income.date).getMonth() + 1;
        const incomeYear = new Date(income.date).getFullYear();

        return incomeMonth == currentMonth && incomeYear == currentYear;
      })
    : null;

  const yearlyIncomeTotals = incomeData
    ? incomeData.filter((income) => {
        const incomeYear = new Date(income.date).getFullYear();

        return incomeYear == currentYear;
      })
    : null;

  const currentMonthExpenses = expenseData
    ? expenseData.filter((expense) => {
        const expenseMonth = new Date(expense.date).getMonth() + 1;
        const expenseYear = new Date(expense.date).getFullYear();

        return expenseMonth == currentMonth && expenseYear == currentYear;
      })
    : null;

  const yearlyExpenseTotals = expenseData
    ? expenseData.filter((expense) => {
        const expenseYear = new Date(expense.date).getFullYear();

        return expenseYear == currentYear;
      })
    : null;

  function handleChangeMonth(event) {
    dispatch(setMonth(event.target.value));
  }

  return (
    <>
      <div className="w-full bg-green-100 flex flex-col p-10 overflow-x-hidden">
        <div className="w-full h-50 flex flex-col lg:flex-row justify-end items-center gap-10 lg:gap-0 lg:items-end lg:justify-between mb-10 px-30">
          <h1 className="text-4xl md:text-6xl xl:text-8xl font-extrabold text-green-700">
            MyDashboard
          </h1>
          <input
            type="month"
            onChange={(event) => handleChangeMonth(event)}
            value={month}
            className=" text-md border-2 rounded-xl md:text-xl xl:text-3xl cursor-pointer font-extrabold p-2"
            max={new Date().toISOString().slice(0, 7)}
            min="2020-01"
          />
        </div>
        <div className="w-full ">
          <DashboardSection
            yearlyTotals={{
              incomes: yearlyIncomeTotals,
              expenses: yearlyExpenseTotals,
            }}
            currentMonthIncomes={currentMonthIncomes}
            currentMonthExpenses={currentMonthExpenses}
          />
        </div>
        <hr className="bg-green-700 h-0.5 w-[70%] mx-auto my-5" />
        <div className="h-full p-3">
          <h1 className="text-6xl text-center font-extrabold text-green-700 mb-10">
            My Transactions
          </h1>
          <div className="flex flex-col items-center justify-evenly md:h-[825px] bg-white rounded-4xl ">
            <TransactionsSection
              incomes={currentMonthIncomes}
              expenses={currentMonthExpenses}
            />
          </div>
        </div>
      </div>
    </>
  );
}
