import { Routes, Route } from "react-router-dom";
import Home from "./pages/Home/Home.jsx";
import Dashboard from "./pages/Dashboard/Dashboard.jsx";
import "./App.css";
import Login from "./pages/Login/Login.jsx";
import SignUp from "./pages/SignUp/SignUp.jsx";
import { AnimatePresence } from "motion/react";
import { useTokenChecker } from "./hooks/useTokenChecker.js";
import { useSelector } from "react-redux";

function App() {
  const userIsLoggedIn = useSelector((state) => state.user.loggedIn);

  if (userIsLoggedIn) {
    useTokenChecker();
  }
  return (
    <AnimatePresence mode="wait">
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<SignUp />} />
        <Route
          path="*"
          element={<p className="errorMessage">404 - Page not found</p>}
        />
      </Routes>
    </AnimatePresence>
  );
}

export default App;
