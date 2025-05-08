import { useLocation } from "react-router-dom";
import Navbar from "./Navbar.jsx";
import HomePageNavbar from "./HomePageNavbar.jsx";
import App from "../App.jsx";

export default function AppWrapper() {
  const location = useLocation();
  const isHome = location.pathname === "/";

  return (
    <>
      {isHome ? <HomePageNavbar /> : <Navbar />}
      <App />
    </>
  );
}
