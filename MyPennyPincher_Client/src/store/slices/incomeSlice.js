import { createSlice } from "@reduxjs/toolkit";

const incomeSlice = createSlice({
  name: "income",
  initialState: {
    incomes: [],
  },
  reducers: {
    setIncomes: (state, action) => {
      state.incomes = action.payload;
    },
    addIncome: (state, action) => {
      state.incomes = [...state.incomes, action.payload];
    },
    editIncome: (state, action) => {
      const index = state.incomes.findIndex(
        (income) => income.incomeId === action.payload.incomeId
      );
      console.log(index);
      if (index !== -1) {
        console.log(state.incomes[index]);
        state.incomes[index] = action.payload;
      }
    },
    deleteIncome: (state, action) => {
      state.incomes = state.incomes.filter(
        (income) => income.incomeId !== action.payload.incomeId
      );
    },
  },
});

export const { setIncomes, addIncome, editIncome, deleteIncome } =
  incomeSlice.actions;
export default incomeSlice.reducer;
