import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./slices/userSlice.js";
import { combineReducers } from "redux";
import monthReducer from "./slices/monthSlice.js";
import incomeReducer from "./slices/incomeSlice.js";
import storage from "redux-persist/lib/storage";
import { persistReducer, persistStore } from "redux-persist";

const persistConfig = {
  key: "root",
  storage,
};

const rootReducer = combineReducers({
  user: userReducer,
  month: monthReducer,
  income: incomeReducer,
});

const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
  reducer: persistedReducer,
});

export const persistor = persistStore(store);
