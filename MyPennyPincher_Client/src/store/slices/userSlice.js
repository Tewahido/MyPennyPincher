import { createSlice } from "@reduxjs/toolkit";

const userSlice = createSlice({
  name: "user",
  initialState: {
    user: null,
    loggedIn: false,
    expiresAt: null,
  },
  reducers: {
    login(state, action) {
      state.user = action.payload;
      state.loggedIn = true;
    },
    logout(state) {
      state.user = null;
      state.loggedIn = false;
      state.expiresAt = null;
    },
    setExpiryTime(state, action) {
      state.expiresAt = action.payload;
    },
  },
});

export const { login, logout, setExpiryTime } = userSlice.actions;
export default userSlice.reducer;
