import { Routes, Route, Navigate } from "react-router-dom";
import Home from "./components/Home.jsx";
import Dashboard from "./components/Dashboard.jsx";
import "./App.css";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/dashboard" element={<Dashboard />} />
      <Route
        path="*"
        element={<p className="errorMessage">404 - Page not found</p>}
      />
    </Routes>
  );
}

export default App;
