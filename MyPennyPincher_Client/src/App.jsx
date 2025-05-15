import { Routes, Route } from "react-router-dom";
import Home from "./pages/Home/Home.jsx";
import Dashboard from "./pages/Dashboard/Dashboard.jsx";
import "./App.css";
import Login from "./pages/Login/Login.jsx";
import SignUp from "./pages/SignUp/SignUp.jsx";
import { AnimatePresence } from "motion/react";

function App() {
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
