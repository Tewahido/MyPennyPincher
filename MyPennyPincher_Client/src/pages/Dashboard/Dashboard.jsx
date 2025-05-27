import DashboardSection from "./components/DashboardSection";
import TransactionsSection from "./components/TransactionsSection";
import { useDispatch, useSelector } from "react-redux";
import { setMonth } from "../../store/slices/monthSlice";
import {
  getMonthTransactions,
  getYearlyTransactions,
} from "../../utils/transactionUtils.js";
import { useFetchUserTransactions } from "../../hooks/useFetchUserTransactions";
import LoadingAnimation from "../../assets/Dashboard_Loading_Animation.json";
import Lottie from "lottie-react";
import { motion } from "motion/react";
import { dashboardFade } from "../../config/animationConfig.js";
import { useFetchExpenseCategories } from "../../hooks/useFetchExpenseCategoriese.js";
import { useNavigate } from "react-router-dom";

export default function Dashboard() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const incomeData = useSelector((state) => state.income.incomes);
  const expenseData = useSelector((state) => state.expense.expenses);
  const month = useSelector((state) => state.month.month);
  const loading = useSelector((state) => state.loading);
  const userLoggedIn = useSelector((state) => state.user.loggedIn);

  if (userLoggedIn) {
    useFetchUserTransactions();
    useFetchExpenseCategories();
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

  function handleChangeMonth(event) {
    dispatch(setMonth(event.target.value));
  }

  return (
    <>
      <div className="w-full h-full bg-green-100 flex flex-col p-10 overflow-x-hidden">
        {loading ? (
          <div className="flex flex-1 justify-center items-center">
            <div className="flex flex-col items-center justify-start">
              <Lottie animationData={LoadingAnimation} />
              <p className="italic text-green-700 text-4xl">Loading...</p>
            </div>
          </div>
        ) : (
          <motion.div
            variants={dashboardFade}
            initial="hidden"
            whileInView="visible"
          >
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
            <div className=" p-3">
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
          </motion.div>
        )}
      </div>
    </>
  );
}
