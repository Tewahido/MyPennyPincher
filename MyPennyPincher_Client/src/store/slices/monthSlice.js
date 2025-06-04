import { createSlice } from "@reduxjs/toolkit";
import { getCurrentMonthAsString } from "../../utils/dateUtils";

const monthSlice = createSlice({
  name: "month",
  initialState: { month: getCurrentMonthAsString() },
  reducers: {
    setMonth(state, action) {
      state.month = action.payload;
    },
    resetMonth(state) {
      state.month = getCurrentMonthAsString();
    },
  },
});

export const { setMonth, resetMonth } = monthSlice.actions;
export default monthSlice.reducer;
