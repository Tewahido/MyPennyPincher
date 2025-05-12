import TransactionEntry from "./TransactionEntry";

export default function TransactionsSection({ incomes, expenses }) {
  return (
    <div className="flex h-full w-full p-3">
      <div className=" flex flex-col h-full w-[50%] p-3">
        <h1 className="text-center text-6xl font-bold text-green-700 mb-10">
          Incomes
        </h1>
        <div className="flex flex-col h-full w-full">
          {incomes.map((income, index) => (
            <TransactionEntry
              type="income"
              entry={income}
              index={index + 1}
              key={income.IncomeId}
            />
          ))}
        </div>
      </div>
      <div className="h-[80%] my-auto w-0.5 bg-green-700"></div>
      <div className=" flex flex-col h-full w-[50%] p-3">
        <h1 className="text-center text-6xl font-bold text-red-700 mb-10">
          Expenses
        </h1>
        {expenses.map((expense, index) => (
          <TransactionEntry
            type="expense"
            entry={expense}
            index={index + 1}
            key={expense.ExpenseId}
          />
        ))}
      </div>
    </div>
  );
}
