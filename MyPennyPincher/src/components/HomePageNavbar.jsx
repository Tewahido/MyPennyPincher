import { Link } from "react-router-dom";
import Logo from "../assets/GrabbingMoneyColor_Icon.png";
import MoneyImage from "../assets/background_image.jpeg";

export default function Navbar() {
  return (
    <div
      className="flex flex-col fixed top-0 left-0 w-full z-10 bg-cover bg-center bg-fixed"
      style={{
        backgroundImage: `url(${MoneyImage})`,
      }}
    >
      <nav className="flex justify-between h-20 mx-10 backdrop-blur-sm">
        <div className="flex items-end h-full ">
          <img
            src={Logo}
            alt="Logo"
            className="mb-3 md:mb-2 lg:mb-0 h-8 md:h-10 lg:h-15 cursor-pointer"
          />
          <Link
            to="/"
            className="text-gray-200 font-bold text-xl md:text-2xl lg:text-3xl mb-3 hover:text-white transition duration-150"
          >
            MyPennyPincher
          </Link>
        </div>
        <div className=" flex items-end h-full gap-10 mr-1 md:mr-3 lg:mr-5">
          <Link
            to="/dashboard"
            className="text-gray-200 font-bold sm:text-lg lg:text-xl mb-3 hover:underline hover:text-white transition duration-300"
          >
            Dashboard
          </Link>
          <Link
            to="/logout"
            className="text-gray-200 font-bold sm:text-lg lg:text-xl mb-3 hover:underline hover:text-white transition duration-300"
          >
            Logout
          </Link>
        </div>
      </nav>
      <div className=" mx-auto h-0.5 w-[90vw] bg-white"></div>
    </div>
  );
}
