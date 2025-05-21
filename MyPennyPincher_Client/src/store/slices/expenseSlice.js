import { createSlice } from "@reduxjs/toolkit";

const expenseSlice = createSlice({
  name: "expense",
  initialState: {
    expenses: [],
  },
  reducers: {
    setExpenses: (state, action) => {
      state.expenses = action.payload;
    },
    addExpense: (state, action) => {
      state.expenses = [...state.expenses, action.payload];
    },
    editExpense: (state, action) => {
      const index = state.expenses.findIndex(
        (expense) => expense.expenseId === action.payload.expenseId
      );
      console.log(index);
      if (index !== -1) {
        console.log(state.expenses[index]);
        state.expenses[index] = action.payload;
      }
    },
    deleteExpense: (state, action) => {
      state.expenses = state.expenses.filter(
        (expense) => expense.expenseId !== action.payload.expenseId
      );
    },
    clearExpenses: (state) => {
      state.expenses = [];
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
