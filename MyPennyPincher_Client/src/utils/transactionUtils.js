export function getMonthTransactions(transactions, currentYear, currentMonth) {
  return transactions
    ? transactions.filter((transaction) => {
        const transactionMonth = new Date(transaction.date).getMonth() + 1;
        const trransactionYear = new Date(transaction.date).getFullYear();

        return (
          transactionMonth == currentMonth && trransactionYear == currentYear
        );
      })
    : null;
}

export function getYearlyTransactions(transactions, currentYear) {
  return transactions
    ? transactions.filter((transaction) => {
        const transactionYear = new Date(transaction.date).getFullYear();

        return transactionYear == currentYear;
      })
    : null;
}

export function getMonthlyTotals(transactions, monthNames) {
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

export function getTransactionsTotal(transactions) {
  return transactions
    ? transactions.reduce((sum, transaction) => sum + +transaction.amount, 0)
    : 0;
}

export function getCategoryTotals(expenses) {
  const rawTotals = expenses.reduce((categoryTotals, expense) => {
    categoryTotals[expense.expenseCategoryId] =
      (categoryTotals[expense.expenseCategoryId] || 0) + expense.amount;

    return categoryTotals;
  }, {});

  let totals = [];

  for (let i = 1; i < 16; i++) {
    totals.push(rawTotals[i] || 0);
  }

  return totals;
}
