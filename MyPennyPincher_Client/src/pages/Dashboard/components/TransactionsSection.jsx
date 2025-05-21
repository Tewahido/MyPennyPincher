import TransactionEntry from "./TransactionEntry";
import ManageIncomeModal from "./ManageIncomeModal";
import ManageExpenseModal from "./ManageExpenseModal";
import AddIncomeIcon from "../../../assets/add_income_icon.png";
import AddExpenseIcon from "../../../assets/add_expense_icon.png";
import { useRef } from "react";

export default function TransactionsSection({ incomes, expenses }) {
  const manageIncomeDialog = useRef();
  const manageExpenseDialog = useRef();

  function handleAddIncome() {
    manageIncomeDialog.current.open();
  }
  function handleAddExpense() {
    manageExpenseDialog.current.open();
  }

  return (
    <>
      <ManageIncomeModal ref={manageIncomeDialog} />
      <ManageExpenseModal ref={manageExpenseDialog} />
      <div className="flex flex-col items-center md:flex-row h-full  w-full p-3">
        <div className="md:h-full w-full md:w-[50%] gap-2">
          <h1 className="text-center text-6xl font-bold text-green-700  m-10">
            Incomes
          </h1>
          <div className=" flex flex-col items-center h-[600px]  gap-2 p-3 overflow-y-auto overflow-x-hidden">
            {incomes &&
              incomes.map((income, index) => (
                <TransactionEntry
                  type="income"
                  entry={income}
                  index={index + 1}
                  key={income.incomeId}
                />
              ))}
            <div className="flex justify-center relative group h-15 my-5 w-21 z-1">
              <img
                src={AddIncomeIcon}
                alt="Plus Icon"
                className="mx-1 h-full cursor-pointer transition duration-300 hover:scale-110 z-10"
                onClick={handleAddIncome}
              />
              <span
                className="absolute bottom-full mb-1 left-1/2  
                 bg-gray-700 text-white text-xs px-2 py-1 rounded 
                 opacity-0 group-hover:opacity-100 transition-opacity duration-400 w-full z-2"
              >
                New Income
              </span>
            </div>
          </div>
        </div>
        <div className="w-[80%] h-0.5 md:h-[80%] my-auto md:w-0.5 bg-green-700"></div>
        <div className="md:h-full w-full md:w-[50%] gap-2">
          <h1 className="text-center text-6xl font-bold text-red-700  m-10">
            Expenses
          </h1>
          <div className=" flex flex-col items-center h-[600px] w-full gap-2 p-3 overflow-y-auto overflow-x-hidden">
            {expenses &&
              expenses.map((expense, index) => (
                <TransactionEntry
                  type="expense"
                  entry={expense}
                  index={index + 1}
                  key={expense.expenseId}
                />
              ))}
            <div className="flex justify-center relative group h-15 my-5 w-22">
              <img
                src={AddExpenseIcon}
                alt="Plus Icon"
                className="mx-1 h-full cursor-pointer transition duration-300 hover:scale-110"
                onClick={handleAddExpense}
              />
              <span
                className="absolute bottom-full mb-1 left-1/2  
                 bg-gray-700 text-white text-xs px-2 py-1 rounded 
                 opacity-0 group-hover:opacity-100 transition-opacity duration-400 w-full"
              >
                New Expense
              </span>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
