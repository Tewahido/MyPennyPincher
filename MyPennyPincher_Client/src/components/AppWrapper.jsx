import { useLocation } from "react-router-dom";
import Navbar from "./Navbar.jsx";
import HomePageNavbar from "./HomePageNavbar.jsx";
import App from "../App.jsx";
import Footer from "./Footer.jsx";
import { useSelector } from "react-redux";
import { useTokenChecker } from "../hooks/useTokenChecker.js";

export default function AppWrapper() {
  const location = useLocation();
  const isHome = location.pathname === "/";
  const loading = useSelector((state) => state.loading);

  useTokenChecker();
  return (
    <div className={`${loading && "flex flex-col h-screen "} font-[Roboto]`}>
      {isHome ? <HomePageNavbar /> : <Navbar />}
      <App />
      <Footer />
    </div>
  );
}
