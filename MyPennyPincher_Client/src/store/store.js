import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./slices/userSlice.js";
import monthReducer from "./slices/monthSlice.js";

export const store = configureStore({
  reducer: {
    user: userReducer,
    month: monthReducer,
  },
});
