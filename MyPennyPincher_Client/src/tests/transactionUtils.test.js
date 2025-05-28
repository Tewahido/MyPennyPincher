import { describe, test } from "vitest";
import { monthNames } from "../constants/monthNames";
import {
  getCategoryTotals,
  getMonthlyTotals,
  getMonthTransactions,
  getTransactionsTotal,
  getYearlyTransactions,
} from "../utils/transactionUtils";

const transactions = [
  { id: 1, date: "2025-05-15", amount: 100, expenseCategoryId: 3 },
  { id: 2, date: "2025-05-20", amount: 50, expenseCategoryId: 7 },
  { id: 3, date: "2025-04-10", amount: 200, expenseCategoryId: 12 },
  { id: 4, date: "2024-05-10", amount: 300, expenseCategoryId: 1 },
];

const currentYearTransactions = [
  { id: 1, date: "2025-05-15", amount: 100, expenseCategoryId: 3 },
  { id: 2, date: "2025-05-20", amount: 50, expenseCategoryId: 7 },
  { id: 3, date: "2025-04-10", amount: 200, expenseCategoryId: 12 },
];

const currentMonth = "05";
const currentYear = "2025";

const invalidMonth = "01";
const invalidYear = "2023";

describe("getMonthTransactions tests", () => {
  test("returns only transactions from May 2025", () => {
    const expected = [
      {
        id: 1,
        date: "2025-05-15",
        amount: 100,
        expenseCategoryId: 3,
      },
      {
        id: 2,
        date: "2025-05-20",
        amount: 50,
        expenseCategoryId: 7,
      },
    ];

    const result = getMonthTransactions(
      transactions,
      currentYear,
      currentMonth
    );

    expect(result).toHaveLength(2);

    expect(result).toEqual(expected);
  });

  test("returns empty array when no transactions match", () => {
    const expected = [];
    const result = getMonthTransactions(
      transactions,
      invalidYear,
      invalidMonth
    );

    expect(result).toEqual(expected);
  });

  test("returns null when transactions is null or undefined", () => {
    const nullTransactionsResult = getMonthTransactions(
      null,
      currentYear,
      currentMonth
    );

    expect(nullTransactionsResult).toBeNull();

    const undefinedTransactionsResult = getMonthTransactions(
      undefined,
      currentYear,
      currentMonth
    );

    expect(undefinedTransactionsResult).toBeNull();
  });
});

describe("getYearlyTransactions tests", () => {
  test("return only transactions from 2025", () => {
    const expected = [
      { id: 1, date: "2025-05-15", amount: 100, expenseCategoryId: 3 },
      { id: 2, date: "2025-05-20", amount: 50, expenseCategoryId: 7 },
      { id: 3, date: "2025-04-10", amount: 200, expenseCategoryId: 12 },
    ];

    const result = getYearlyTransactions(transactions, currentYear);

    expect(result).toHaveLength(3);

    expect(result).toEqual(expected);
  });

  test("returns empty array when no transactions match", () => {
    const expected = [];

    const result = getYearlyTransactions(transactions, invalidYear);

    expect(result).toEqual(expected);
  });

  test("returns null when transactions is null or undefined", () => {
    const nullTransactionsResult = getYearlyTransactions(null, currentYear);

    expect(nullTransactionsResult).toBeNull();

    const undefinedTransactionsResult = getYearlyTransactions(
      undefined,
      currentYear
    );

    expect(undefinedTransactionsResult).toBeNull();
  });
});

describe("getMonthlyTotals tests", () => {
  test("returns totals for each month", () => {
    const expected = [0, 0, 0, 200, 150, 0, 0, 0, 0, 0, 0, 0];

    const result = getMonthlyTotals(currentYearTransactions, monthNames);

    expect(result).toHaveLength(12);

    expect(result).toEqual(expected);
  });

  test("returns null when transactions is null or undefined", () => {
    const nullTransactionsResult = getMonthlyTotals(null, monthNames);

    expect(nullTransactionsResult).toBeNull();

    const undefinedTransactionsResult = getMonthlyTotals(undefined, monthNames);

    expect(undefinedTransactionsResult).toBeNull();
  });
});

describe("getTransactionsTotal tests", () => {
  test("returns sum of all transactions amounts", () => {
    const result = getTransactionsTotal(transactions);

    expect(result).toEqual(650);
  });

  test("returns 0 when transactions are null or undefined", () => {
    const nullTransactionsResult = getTransactionsTotal(null);

    expect(nullTransactionsResult).toEqual(0);

    const undefinedTransactionsResult = getTransactionsTotal(undefined);

    expect(undefinedTransactionsResult).toEqual(0);
  });
});

describe("getCategoryTotals tests", () => {
  test("returns totals for each category", () => {
    const expected = [300, 0, 100, 0, 0, 0, 50, 0, 0, 0, 0, 200, 0, 0, 0];

    const result = getCategoryTotals(transactions);

    expect(result).toHaveLength(15);

    expect(result).toEqual(expected);
  });
});
