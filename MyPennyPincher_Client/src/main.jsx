import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import AppWrapper from "./components/AppWrapper.jsx";
import ScrollToTop from "./components/ScrollToTop.jsx";
import { store } from "./store/store.js";
import { Provider } from "react-redux";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <BrowserRouter>
      <Provider store={store}>
        <ScrollToTop />
        <AppWrapper />
      </Provider>
    </BrowserRouter>
  </StrictMode>
);
