import DashboardSection from "./components/DashboardSection";
import TransactionsSection from "./components/TransactionsSection";
import { useDispatch, useSelector } from "react-redux";
import { setMonth } from "../../store/slices/monthSlice";
import { useNavigate } from "react-router-dom";
import {
  getMonthTransactions,
  getYearlyTransactions,
  logoutUser,
} from "../../util/util";
import { useFetchUserTransactions } from "../../hooks/useFetchUserTransactions";

//   {
//     expenseId: 1,
//     description: "Rent",
//     amount: 1200,
//     date: "2025-01-01",
//     recurring: true,
//     categoryId: 1,
//     userId: 1,
//   },
//   {
//     expenseId: 2,
//     description: "Electricity Bill",
//     amount: 100,
//     date: "2025-02-05",
//     recurring: true,
//     categoryId: 2,
//     userId: 1,
//   },
//   {
//     expenseId: 3,
//     description: "Groceries",
//     amount: 450,
//     date: "2025-03-10",
//     recurring: true,
//     categoryId: 3,
//     userId: 1,
//   },
//   {
//     expenseId: 4,
//     description: "New Phone",
//     amount: 900,
//     date: "2025-04-15",
//     recurring: false,
//     categoryId: 4,
//     userId: 1,
//   },
//   {
//     expenseId: 5,
//     description: "Vacation Trip",
//     amount: 1500,
//     date: "2025-05-02",
//     recurring: false,
//     categoryId: 5,
//     userId: 1,
//   },
//   {
//     expenseId: 6,
//     description: "Internet Subscription",
//     amount: 60,
//     date: "2025-01-10",
//     recurring: true,
//     categoryId: 2,
//     userId: 1,
//   },
//   {
//     expenseId: 7,
//     description: "Gym Membership",
//     amount: 50,
//     date: "2025-02-12",
//     recurring: true,
//     categoryId: 6,
//     userId: 1,
//   },
//   {
//     expenseId: 8,
//     description: "Movie Night",
//     amount: 30,
//     date: "2025-03-21",
//     recurring: false,
//     categoryId: 7,
//     userId: 1,
//   },
//   {
//     expenseId: 9,
//     description: "Dining Out",
//     amount: 75,
//     date: "2025-04-10",
//     recurring: false,
//     categoryId: 7,
//     userId: 1,
//   },
//   {
//     expenseId: 10,
//     description: "Clothing",
//     amount: 200,
//     date: "2025-05-08",
//     recurring: false,
//     categoryId: 8,
//     userId: 1,
//   },
// ];

export default function Dashboard() {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const incomeData = useSelector((state) => state.income.incomes);
  const expenseData = useSelector((state) => state.expense.expenses);
  const month = useSelector((state) => state.month.month);
  const tokenExpiryTime = useSelector((state) => state.user.expiresAt);

  const convertedTokenExpiryTime = new Date(tokenExpiryTime);

  if (Date.now() >= new Date(convertedTokenExpiryTime).getTime()) {
    logoutUser(dispatch, navigate);
  }

  const [currentYear, currentMonth] = month.split("-");

  const currentMonthIncomes = getMonthTransactions(
    incomeData,
    currentYear,
    currentMonth
  );

  const currentMonthExpenses = getMonthTransactions(
    expenseData,
    currentYear,
    currentMonth
  );

  const currentYearIncomes = getYearlyTransactions(incomeData, currentYear);

  const currentYearExpenses = getYearlyTransactions(expenseData, currentYear);

  useFetchUserTransactions();

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
            max={`${currentYear}-12`}
            min="2020-01"
          />
        </div>
        <div className="w-full ">
          <DashboardSection
            yearlyTotals={{
              incomes: currentYearIncomes,
              expenses: currentYearExpenses,
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
