import DiagonalLine from "./DiagonalLine";
import SniperReticle from "./SniperReticle";
import MoneyImage from "../../../assets/background_image.jpeg";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";

export default function HeroBanner({ servicesRef }) {
  const userLoggedIn = useSelector((state) => state.user.loggedIn);

  const handleClick = () => {
    const element = servicesRef.current;

    if (element) {
      const offsetTop = element.getBoundingClientRect().top + window.scrollY;
      const scrollTarget = offsetTop - window.innerHeight * 0.3;

      window.scrollTo({
        top: scrollTarget,
        behavior: "smooth",
      });
    }
  };

  return (
    <section className="flex flex-col lg:flex-row justify-center lg:h-200 w-full mt-20 text-white">
      <div className="flex flex-col w-[100%] justify-center items-center p-10 text-center">
        <h1 className="  text-3xl md:text-5xl lg:text-6xl font-extrabold p-10  ">
          Your all-in-one personal finance manager
        </h1>
        <hr className="flex h-0.5 w-[75%] mx-auto m-7.5 bg-white" />
        <p className=" text-lg font-bold w-[80%]">
          Easily track your money <br /> going in or out <br /> without a doubt
        </p>

        <div className="flex flex-row h-15 justify-center items-center w-full mt-10">
          <Link
            to={!userLoggedIn ? "/login" : "/dashboard"}
            className="flex items-center justify-center h-full w-35 bg-white rounded-lg cursor-pointer text-green-800 font-bold border-2 border-green-850 transition-all duration-200 hover:bg-green-800 hover: hover:border-white hover:text-white"
          >
            Get Started
          </Link>

          <p
            onClick={handleClick}
            className="underline underline-offset-2 decoration-0 m-5 text-center cursor-pointer transition-all duration-300 hover:scale-105 hover:text-white hover:underline-offset-4"
          >
            View our services
          </p>
        </div>
      </div>
      <div className="h-[70%] w-100 justify-center items-center my-auto hidden lg:flex">
        <DiagonalLine color="white" />
      </div>
      <div className="flex flex-col w-[100%] items-center justify-center ">
        <div className="flex flex-col p-5 w-full justify-start items-center  z-1">
          <h1 className=" w-[80%] font-extrabold  text-3xl md:text-5xl lg:text-4xl text-center">
            Never lose sight of what Really matters.
          </h1>
        </div>
        <div
          className="w-45 md:w-65 lg:w-75 h-45 md:h-65 lg:h-75 rounded-full bg-top bg-contain"
          style={{
            backgroundImage: `url(${MoneyImage})`,
          }}
        >
          <SniperReticle />
        </div>
        <h1 className="w-[70%] text-shadow-black font-extrabold text-2xl md:text-3xl lg:text-4xl text-center">
          And <span className="italic font-bold">Always</span> Hit Your Target.
        </h1>
      </div>
    </section>
  );
}
