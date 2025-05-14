import { useLocation } from "react-router-dom";
import Navbar from "./Navbar.jsx";
import HomePageNavbar from "./HomePageNavbar.jsx";
import App from "../App.jsx";
import Footer from "./Footer.jsx";
import { Provider } from "react-redux";
import { store } from "../store/store.js";

export default function AppWrapper() {
  const location = useLocation();
  const isHome = location.pathname === "/";

  return (
    <div className="font-[Roboto]">
      <Provider store={store}>
        {isHome ? <HomePageNavbar /> : <Navbar />}
        <App />
        <Footer />
      </Provider>
    </div>
  );
}
