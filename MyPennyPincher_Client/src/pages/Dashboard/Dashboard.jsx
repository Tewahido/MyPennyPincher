import DashboardSection from "./components/DashboardSection";
import TransactionsSection from "./components/TransactionsSection";
import { useDispatch, useSelector } from "react-redux";
import { setMonth } from "../../store/slices/monthSlice";
import {
  getMonthRangeTransactions,
  getMonthTransactions,
  getYearlyTransactions,
} from "../../utils/transactionUtils.js";
import { useFetchUserTransactions } from "../../hooks/useFetchUserTransactions";
import LoadingAnimation from "../../assets/Dashboard_Loading_Animation.json";
import Lottie from "lottie-react";
import { motion } from "motion/react";
import { dashboardFade } from "../../config/animationConfig.js";
import { useState } from "react";
import {
  setFromMonth,
  setToMonth,
} from "../../store/slices/monthRangeSlice.js";

export default function Dashboard() {
  const dispatch = useDispatch();

  const [transactionPeriod, setTransactionPeriod] = useState("month");

  const incomeData = useSelector((state) => state.income.incomes);
  const expenseData = useSelector((state) => state.expense.expenses);
  const month = useSelector((state) => state.month.month);
  const monthRange = useSelector((state) => state.monthRange);
  const loading = useSelector((state) => state.loading);

  useFetchUserTransactions();

  const [currentYear, currentMonth] = month.split("-");
  const [currentFromyear, currentFromMonth] = monthRange.fromMonth.split("-");
  const [currentToyear, currentToMonth] = monthRange.toMonth.split("-");

  const currentPeriodIncomes =
    transactionPeriod == "month"
      ? getMonthTransactions(incomeData, currentYear, currentMonth)
      : getMonthRangeTransactions(
          incomeData,
          currentFromyear,
          currentFromMonth,
          currentToyear,
          currentToMonth
        );

  const currentPeriodExpenses =
    transactionPeriod == "month"
      ? getMonthTransactions(expenseData, currentYear, currentMonth)
      : getMonthRangeTransactions(
          expenseData,
          currentFromyear,
          currentFromMonth,
          currentToyear,
          currentToMonth
        );

  const currentYearIncomes = getYearlyTransactions(incomeData, currentYear);

  const currentYearExpenses = getYearlyTransactions(expenseData, currentYear);

  function firstMonthPickerHandleChange(event) {
    if (transactionPeriod === "month") {
      handleChangeMonth(event);
    } else {
      handleFromMonthChange(event);
    }
  }

  function handleChangeMonth(event) {
    dispatch(setMonth(event.target.value));
  }

  function handleTransactionPeriodChange(event) {
    setTransactionPeriod(event.target.value);
  }

  function handleFromMonthChange(event) {
    dispatch(setFromMonth(event.target.value));
  }

  function handleToMonthChange(event) {
    dispatch(setToMonth(event.target.value));
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
            <div className="w-full h-50 flex flex-col lg:flex-row justify-end items-center gap-10 lg:gap-0 lg:items-end lg:justify-between my-10 px-30">
              <h1 className="text-4xl md:text-6xl xl:text-8xl font-extrabold text-green-700">
                MyDashboard
              </h1>
              <div className="flex flex-col items-end gap-2 md:text-md xl:text-lg ">
                <div className="flex items-center gap-3">
                  <p className="font-bold">Transaction Period:</p>
                  <select
                    name="transactionPeriod"
                    className="border-1 border-black p-2 rounded-md w-40 cursor-pointer bg-white text-black font-bold focus:outline-none"
                    value={transactionPeriod}
                    onChange={handleTransactionPeriodChange}
                  >
                    <option value="month">Single Month</option>
                    <option value="monthRange">Month Range</option>
                  </select>
                </div>
                <div className="flex items-center gap-2">
                  <input
                    type="month"
                    onChange={(event) => firstMonthPickerHandleChange(event)}
                    value={
                      transactionPeriod == "month"
                        ? month
                        : monthRange.fromMonth
                    }
                    className="text-md border-1 bg-white rounded-md cursor-pointer font-extrabold p-2"
                    max={
                      transactionPeriod == "month"
                        ? `${currentYear}-12`
                        : monthRange.toMonth
                    }
                    min="2025-01"
                  />
                  {transactionPeriod == "monthRange" && (
                    <>
                      <p className="font-bold text-xl">---</p>
                      <input
                        type="month"
                        onChange={(event) => handleToMonthChange(event)}
                        value={monthRange.toMonth}
                        className="text-md border-1 bg-white rounded-md cursor-pointer font-extrabold p-2"
                        max={`${currentYear}-12`}
                        min={
                          transactionPeriod == "month"
                            ? "2025-01"
                            : monthRange.fromMonth
                        }
                      />
                    </>
                  )}
                </div>
              </div>
            </div>
            <div className="w-full">
              <DashboardSection
                yearlyTotals={{
                  incomes: currentYearIncomes,
                  expenses: currentYearExpenses,
                }}
                currentMonthIncomes={currentPeriodIncomes}
                currentMonthExpenses={currentPeriodExpenses}
              />
            </div>
            <hr className="bg-green-700 h-0.5 w-[70%] mx-auto my-5" />
            <div className=" p-3">
              <h1 className="text-6xl text-center font-extrabold text-green-700 mb-10">
                My Transactions
              </h1>
              <div className="flex flex-col items-center justify-evenly md:h-[825px] bg-white rounded-4xl ">
                <TransactionsSection
                  incomes={currentPeriodIncomes}
                  expenses={currentPeriodExpenses}
                />
              </div>
            </div>
          </motion.div>
        )}
      </div>
    </>
  );
}
