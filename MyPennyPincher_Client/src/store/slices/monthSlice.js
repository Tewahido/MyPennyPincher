import { createSlice } from "@reduxjs/toolkit";

function getCurrentMonthString() {
  const now = new Date();
  const year = now.getFullYear();
  const month = (now.getMonth() + 1).toString().padStart(2, "0");
  return `${year}-${month}`;
}

const monthSlice = createSlice({
  name: "month",
  initialState: { month: getCurrentMonthString() },
  reducers: {
    setMonth(state, action) {
      state.month = action.payload;
    },
    resetMonth(state) {
      state.month = getCurrentMonthString();
    },
  },
});

export const { setMonth, resetMonth } = monthSlice.actions;
export default monthSlice.reducer;
