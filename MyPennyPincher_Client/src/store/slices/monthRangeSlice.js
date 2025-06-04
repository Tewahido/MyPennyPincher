import { createSlice } from "@reduxjs/toolkit";
import { getCurrentMonthAsString } from "../../utils/dateUtils";

const monthRangeSlice = createSlice({
  name: "monthRange",
  initialState: {
    fromMonth: getCurrentMonthAsString(),
    toMonth: getCurrentMonthAsString(),
  },
  reducers: {
    setFromMonth(state, action) {
      state.fromMonth = action.payload;
    },
    setToMonth(state, action) {
      state.toMonth = action.payload;
    },
    resetMonthRange(state) {
      state.fromMonth = getCurrentMonthAsString();
      state.toMonth = getCurrentMonthAsString();
    },
  },
});

export const { setFromMonth, setToMonth, resetMonthRange } =
  monthRangeSlice.actions;
export default monthRangeSlice.reducer;
