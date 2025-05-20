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
  },
});

export const { setMonth } = monthSlice.actions;
export default monthSlice.reducer;
