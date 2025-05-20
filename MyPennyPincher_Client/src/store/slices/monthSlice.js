import { createSlice } from "@reduxjs/toolkit";

const monthSlice = createSlice({
  name: "month",
  initialState: { month: null },
  reducers: {
    setMonth(state, action) {
      state.month = action.payload;
    },
  },
});

export const { setMonth } = monthSlice.actions;
export default monthSlice.reducer;
