import { Link, useLocation, useNavigate } from "react-router-dom";
import Logo from "/GrabbingMoneyColor_Icon.png";
import GreenBg from "../assets/Plain_Green_Background_Wallpaper.jpg";
import { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { logoutUser } from "../utils/authUtils";

export default function Navbar() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const location = useLocation();
  const user = useSelector((state) => state.user.user);
  const userIsLoggedIn = useSelector((state) => state.user.loggedIn);

  const [scrolled, setScrolled] = useState(false);

  useEffect(() => {
    function handleScroll() {
      setScrolled(window.scrollY > 0);
    }

    window.addEventListener("scroll", handleScroll);

    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, []);

  function handleLogout() {
    if (userIsLoggedIn) {
      logoutUser(dispatch, navigate, location, user.userId);
    }
  }

  return (
    <div
      className="flex flex-col fixed top-0 left-0 w-full z-10 bg-cover bg-center bg-fixed "
      style={{
        backgroundImage: `url(${GreenBg})`,
      }}
    >
      <nav className="flex justify-between h-20 mx-5 md:mx-10 lg:mx-25 backdrop-blur-sm">
        <div className="flex items-end h-full ">
          <img
            src={Logo}
            alt="Logo"
            className="mb-3 md:mb-2 lg:mb-0 h-8 md:h-10 lg:h-15 cursor-pointer"
          />
          <Link
            to="/"
            className="text-gray-300 font-bold text-xl md:text-2xl lg:text-3xl mb-3 hover:text-white transition duration-150 "
          >
            MyPennyPincher
          </Link>
        </div>
        <div className=" flex items-end h-full gap-10 mr-1 md:mr-3 lg:mr-5">
          {userIsLoggedIn && (
            <Link
              to="/dashboard"
              className="text-gray-100 font-bold sm:text-lg lg:text-xl mb-3 hover:underline hover:text-white transition duration-300"
            >
              Dashboard
            </Link>
          )}
          <Link
            to="/login"
            className="text-gray-100 font-bold sm:text-lg lg:text-xl mb-3 hover:underline hover:text-white transition duration-300"
            onClick={handleLogout}
          >
            {userIsLoggedIn ? "Logout" : "Login"}
          </Link>
        </div>
      </nav>
      {scrolled && <div className=" mx-auto h-0.5 w-[90vw] bg-gray-200"></div>}
    </div>
  );
}
