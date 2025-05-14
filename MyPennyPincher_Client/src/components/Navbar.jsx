import { Link } from "react-router-dom";
import Logo from "/GrabbingMoneyColor_Icon.png";
import { useState, useEffect } from "react";
import { useSelector } from "react-redux";

export default function Navbar() {
  const user = useSelector((state) => state.user);

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

  return (
    <div className="flex flex-col fixed top-0 left-0 w-full z-10 bg-green-100">
      <nav className="flex justify-between h-20 mx-25 ">
        <div className="flex items-end h-full ">
          <img
            src={Logo}
            alt="Logo"
            className="mb-3 md:mb-2 lg:mb-0 h-8 md:h-10 lg:h-15 cursor-pointer"
          />
          <Link
            to="/"
            className="text-green-700 font-bold text-xl md:text-2xl lg:text-3xl mb-3 hover:text-green-600 transition duration-150"
          >
            MyPennyPincher
          </Link>
        </div>
        <div className=" flex items-end h-full gap-10 mr-1 md:mr-3 lg:mr-5">
          <Link
            to="/dashboard"
            className="text-green-700 font-bold sm:text-lg lg:text-xl mb-3 hover:underline hover:text-green-600 transition duration-300"
          >
            Dashboard
          </Link>
          <Link
            to={user.loggedIn ? "/logout" : "/login"}
            className="text-green-700 font-bold sm:text-lg lg:text-xl mb-3 hover:underline hover:text-green-600 transition duration-300"
          >
            {user.loggedIn ? "Logout" : "Login"}
          </Link>
        </div>
      </nav>
      {scrolled && <div className=" mx-auto h-0.5 w-[90vw] bg-green-700"></div>}
    </div>
  );
}
