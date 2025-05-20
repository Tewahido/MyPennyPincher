import { useEffect, useState } from "react";
import DashboardSection from "./components/DashboardSection";
import TransactionsSection from "./components/TransactionsSection";
import { useDispatch, useSelector } from "react-redux";
import { setMonth } from "../../store/slices/monthSlice";
import { GetUserIncomes } from "../../services/incomeService";
import { setIncomes } from "../../store/slices/incomeSlice";

// const incomeData = [
//   {
//     IncomeId: 1,
//     Source: "Salary",
//     Amount: 5000,
//     Date: "2025-01-25",
//     Monthly: true,
//     UserId: 1,
//   },
//   {
//     IncomeId: 2,
//     Source: "Freelance Project",
//     Amount: 1500,
//     Date: "2025-02-10",
//     Monthly: false,
//     UserId: 1,
//   },
//   {
//     IncomeId: 3,
//     Source: "Stock Dividends",
//     Amount: 300,
//     Date: "2025-03-18",
//     Monthly: false,
//     UserId: 1,
//   },
//   {
//     IncomeId: 4,
//     Source: "Bonus",
//     Amount: 2000,
//     Date: "2025-04-05",
//     Monthly: false,
//     UserId: 1,
//   },
//   {
//     IncomeId: 5,
//     Source: "Gift",
//     Amount: 800,
//     Date: "2025-05-12",
//     Monthly: false,
//     UserId: 1,
//   },
//   {
//     IncomeId: 6,
//     Source: "Side Hustle",
//     Amount: 400,
//     Date: "2025-01-15",
//     Monthly: false,
//     UserId: 1,
//   },
//   {
//     IncomeId: 7,
//     Source: "Selling Old Laptop",
//     Amount: 600,
//     Date: "2025-02-22",
//     Monthly: false,
//     UserId: 1,
//   },
//   {
//     IncomeId: 8,
//     Source: "Freelance Design",
//     Amount: 700,
//     Date: "2025-03-03",
//     Monthly: false,
//     UserId: 1,
//   },
//   {
//     IncomeId: 9,
//     Source: "Cashback",
//     Amount: 100,
//     Date: "2025-04-20",
//     Monthly: false,
//     UserId: 1,
//   },
//   {
//     IncomeId: 10,
//     Source: "Consulting",
//     Amount: 1200,
//     Date: "2025-05-05",
//     Monthly: false,
//     UserId: 1,
//   },
// ];

const expenseData = [
  {
    ExpenseId: 1,
    Description: "Rent",
    Amount: 1200,
    Date: "2025-01-01",
    Recurring: true,
    CategoryId: 1,
    UserId: 1,
  },
  {
    ExpenseId: 2,
    Description: "Electricity Bill",
    Amount: 100,
    Date: "2025-02-05",
    Recurring: true,
    CategoryId: 2,
    UserId: 1,
  },
  {
    ExpenseId: 3,
    Description: "Groceries",
    Amount: 450,
    Date: "2025-03-10",
    Recurring: true,
    CategoryId: 3,
    UserId: 1,
  },
  {
    ExpenseId: 4,
    Description: "New Phone",
    Amount: 900,
    Date: "2025-04-15",
    Recurring: false,
    CategoryId: 4,
    UserId: 1,
  },
  {
    ExpenseId: 5,
    Description: "Vacation Trip",
    Amount: 1500,
    Date: "2025-05-02",
    Recurring: false,
    CategoryId: 5,
    UserId: 1,
  },
  {
    ExpenseId: 6,
    Description: "Internet Subscription",
    Amount: 60,
    Date: "2025-01-10",
    Recurring: true,
    CategoryId: 2,
    UserId: 1,
  },
  {
    ExpenseId: 7,
    Description: "Gym Membership",
    Amount: 50,
    Date: "2025-02-12",
    Recurring: true,
    CategoryId: 6,
    UserId: 1,
  },
  {
    ExpenseId: 8,
    Description: "Movie Night",
    Amount: 30,
    Date: "2025-03-21",
    Recurring: false,
    CategoryId: 7,
    UserId: 1,
  },
  {
    ExpenseId: 9,
    Description: "Dining Out",
    Amount: 75,
    Date: "2025-04-10",
    Recurring: false,
    CategoryId: 7,
    UserId: 1,
  },
  {
    ExpenseId: 10,
    Description: "Clothing",
    Amount: 200,
    Date: "2025-05-08",
    Recurring: false,
    CategoryId: 8,
    UserId: 1,
  },
];

export default function Dashboard() {
  const incomeData = useSelector((state) => state.income.incomes);
  const user = useSelector((state) => state.user.user);

  const [selectedMonth, setSelectedMonth] = useState("2025-05");

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(setMonth(selectedMonth));

    async function fetchIncomes() {
      const userIncomes = await GetUserIncomes(user.userId, user.token);
      dispatch(setIncomes(userIncomes));
    }
    fetchIncomes();
  }, []);

  const [currentYear, currentMonth] = selectedMonth.split("-");
  const currentMonthIncomes = incomeData
    ? incomeData.filter((income) => {
        const incomeMonth = new Date(income.date).getMonth() + 1;
        const incomeYear = new Date(income.date).getFullYear();

        return incomeMonth == currentMonth && incomeYear == currentYear;
      })
    : null;

  console.log(currentMonthIncomes);

  const currentMonthExpenses = expenseData
    ? expenseData.filter((expense) => {
        const expenseMonth = new Date(expense.date).getMonth() + 1;
        const expenseYear = new Date(expense.date).getFullYear();

        return expenseMonth == currentMonth && expenseYear == currentYear;
      })
    : null;

  function handleChangeMonth(event) {
    setSelectedMonth(event.target.value);
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
            value={selectedMonth}
            className=" text-md border-2 rounded-xl md:text-xl xl:text-3xl cursor-pointer font-extrabold p-2"
            max={new Date().toISOString().slice(0, 7)}
            min="2020-01"
          />
        </div>
        <div className="w-full ">
          <DashboardSection
            totals={{ incomes: incomeData, expenses: expenseData }}
            currentMonthIncomes={currentMonthIncomes}
            currentMonthExpenses={currentMonthExpenses}
          />
        </div>
        <hr className="bg-green-700 h-0.5 w-[70%] mx-auto my-5" />
        <div className="p-3">
          <h1 className="text-6xl text-center font-extrabold text-green-700 mb-10">
            My Transactions
          </h1>
          <div className="flex flex-col items-center bg-white h-200 rounded-4xl ">
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
