import { createSlice } from "@reduxjs/toolkit";

const expenseSlice = createSlice({
  name: "expense",
  initialState: {
    expenses: [],
    reloadExpenses: false,
  },
  reducers: {
    setExpenses: (state, action) => {
      state.expenses = action.payload;
    },
    addExpense: (state, action) => {
      state.expenses = [...state.expenses, action.payload];
      state.reloadExpenses = !state.reloadExpenses;
    },
    editExpense: (state, action) => {
      state.expenses = state.expenses.map((expense) =>
        expense.expenseId === action.payload.expenseId
          ? action.payload
          : expense
      );
      state.reloadExpenses = !state.reloadExpenses;
    },
    deleteExpense: (state, action) => {
      state.expenses = state.expenses.filter(
        (expense) => expense.expenseId !== action.payload.expenseId
      );
      state.reloadExpenses = !state.reloadExpenses;
    },
    clearExpenses: (state) => {
      state.expenses = [];
      state.reloadExpenses = !state.reloadExpenses;
    },
  },
});

export const {
  setExpenses,
  addExpense,
  editExpense,
  deleteExpense,
  clearExpenses,
} = expenseSlice.actions;
export default expenseSlice.reducer;
