import DashboardSection from "./components/DashboardSection";
import TransactionsSection from "./components/TransactionsSection";
import { useSelector } from "react-redux";
import { motion } from "framer-motion";
import { dashboardFade } from "../../config/animationConfig.js";
import useTransactionPeriod from "../../hooks/useTransactionPeriod.js";
import useFetchUserExpenses from "../../hooks/useFetchUserExpenses.js";
import useFetchUserIncomes from "../../hooks/useFetchUserIncomes.js";

export default function Dashboard() {
  const {
    periodStart,
    periodEnd,
    month,
    monthRange,
    transactionPeriod,
    currentYear,
    currentFromYear,
    TransactionPeriod,
    firstMonthPickerHandleChange,
    handleTransactionPeriodChange,
    handleToMonthChange,
  } = useTransactionPeriod();

  const token = useSelector((state) => state.user.user.token);

  const limit = 10;
  const offset = 0;

  const yearForTransactions =
    transactionPeriod == TransactionPeriod.MONTH
      ? currentYear
      : currentFromYear;

  const {
    expensesForPeriod: expenseData,
    yearExpenses: currentYearExpenses,
    loading: loadingExpenses,
  } = useFetchUserExpenses(
    periodStart,
    periodEnd,
    limit,
    offset,
    yearForTransactions,
    token
  );

  const {
    incomesForPeriod: incomeData,
    yearIncomes: currentYearIncomes,
    loading: loadingIncomes,
  } = useFetchUserIncomes(
    periodStart,
    periodEnd,
    limit,
    offset,
    yearForTransactions,
    token
  );

  const loading = loadingExpenses || loadingIncomes;

  return (
    <>
      <div className="w-full h-full bg-green-100 flex flex-col p-10 overflow-x-hidden">
        <motion.div
          variants={dashboardFade}
          initial="hidden"
          whileInView="visible"
        >
          <div className="w-full h-50 flex flex-col lg:flex-row justify-end items-center gap-10 lg:gap-0 lg:items-end lg:justify-between my-10 px-30">
            <h1 className="text-4xl md:text-6xl xl:text-8xl font-extrabold text-green-700">
              MyDashboard
            </h1>
            <div className="flex flex-col items-end gap-2 md:text-md xl:text-lg">
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
                    transactionPeriod == TransactionPeriod.MONTH
                      ? month
                      : monthRange.fromMonth
                  }
                  className="text-md border-1 bg-white rounded-md cursor-pointer font-extrabold p-2"
                  max={
                    transactionPeriod == TransactionPeriod.MONTH
                      ? `${currentYear}-12`
                      : monthRange.toMonth
                  }
                  min="2025-01"
                />
                {transactionPeriod == TransactionPeriod.MONTH_RANGE && (
                  <>
                    <p className="font-bold text-xl">---</p>
                    <input
                      type="month"
                      onChange={(event) => handleToMonthChange(event)}
                      value={monthRange.toMonth}
                      className="text-md border-1 bg-white rounded-md cursor-pointer font-extrabold p-2"
                      max={`${currentYear}-12`}
                      min={
                        transactionPeriod == TransactionPeriod.MONTH
                          ? `${currentYear}-01`
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
              currentMonthIncomes={incomeData}
              currentMonthExpenses={expenseData}
            />
          </div>
          <hr className="bg-green-700 h-0.5 w-[70%] mx-auto my-5" />
          <div className=" p-3">
            <h1 className="text-6xl text-center font-extrabold text-green-700 mb-10">
              My Transactions
            </h1>
            <div className="flex flex-col items-center justify-evenly md:h-[825px] bg-white rounded-4xl">
              <TransactionsSection
                incomes={incomeData}
                expenses={expenseData}
              />
            </div>
          </div>
        </motion.div>
      </div>
    </>
  );
}
