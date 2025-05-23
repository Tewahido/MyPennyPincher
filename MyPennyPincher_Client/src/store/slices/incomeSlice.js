import { createSlice } from "@reduxjs/toolkit";

const incomeSlice = createSlice({
  name: "income",
  initialState: {
    incomes: [],
    reloadIncomes: false,
  },
  reducers: {
    setIncomes: (state, action) => {
      state.incomes = [...action.payload];
    },
    addIncome: (state, action) => {
      state.incomes = [...state.incomes, action.payload];
      state.reloadIncomes = !state.reloadIncomes;
    },
    editIncome: (state, action) => {
      state.incomes = state.incomes.map((income) =>
        income.incomeId == action.payload.incomeId ? action.payload : income
      );
      state.reloadIncomes = !state.reloadIncomes;
    },
    deleteIncome: (state, action) => {
      state.incomes = state.incomes.filter(
        (income) => income.incomeId != action.payload.incomeId
      );
      state.reloadIncomes = !state.reloadIncomes;
    },
    clearIncomes: (state) => {
      state.incomes = [];
      state.reloadIncomes = !state.reloadIncomes;
    },
  },
});

export const { setIncomes, addIncome, editIncome, deleteIncome, clearIncomes } =
  incomeSlice.actions;
export default incomeSlice.reducer;
