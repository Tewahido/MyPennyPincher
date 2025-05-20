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
    updateIncome: (state, action) => {
      const index = state.incomes.findIndex((i) => i.id === action.payload.id);
      if (index !== -1) {
        state.incomes[index] = action.payload;
      }
    },
    deleteIncome: (state, action) => {
      state.incomes = state.incomes.filter((i) => i.id !== action.payload);
    },
  },
});

export const { setIncomes, setLoading, setError } = incomeSlice.actions;
export default incomeSlice.reducer;
