import MoneyImage from "../assets/background_image.jpeg";
import SniperReticle from "./SniperReticle.jsx";
import GreenBg from "../assets/Plain_Green_Background_Wallpaper.jpg";

import DiagonalLine from "./DiagonalLine.jsx";

export default function Home() {
  return (
    <>
      <div className="w-full ">
        <div
          className="flex flex-col items-center w-full capitalize bg-cover bg-center bg-fixed"
          style={{
            backgroundImage: `url(${GreenBg})`,
          }}
        >
          <div className="backdrop-blur-3xl w-full h-full">
            <div className="flex justify-center h-200 w-full mt-20 ">
              <div className="flex flex-col w-[50vw] justify-center items-center p-10 text-center ">
                <h1 className=" text-white text-xl sm:text-2xl md:text-4xl lg:text-6xl font-extrabold p-10  ">
                  Your all-in-one personal finance manager
                </h1>
                <hr className="flex h-0.5 w-[75%] mx-auto m-7.5 bg-white" />
                <p className="text-white text-lg font-bold w-[80%]">
                  Easily track of all your money going in or out <br /> without
                  a doubt
                </p>

                <div className="flex flex-row h-15 justify-center items-center w-full mt-10">
                  <button className="h-full w-35 bg-white rounded-lg cursor-pointer text-green-800 font-bold border-2 border-green-850 transition-all duration-200 hover:bg-green-800 hover:text-white hover:border-white">
                    Get Started
                  </button>

                  <p className="text-white underline m-5 text-center cursor-pointer transition-all duration-150 hover:scale-105">
                    View our services
                  </p>
                </div>
              </div>

              <div className="flex h-[70%] justify-center items-center my-auto">
                <DiagonalLine color="white" />
              </div>
              <div className="flex flex-col w-[50%] items-center justify-center">
                <div className="flex flex-col p-5   w-full justify-start items-center  z-1">
                  <h1 className="text-white w-[70%] text-shadow-black font-extrabold text-2xl md:text-3xl lg:text-4xl text-center ">
                    Never lose sight of what Really matters.
                  </h1>
                </div>
                <div
                  className="w-45 md:w-65 lg:w-75 h-45 md:h-65 lg:h-75 rounded-full bg-top bg-contain "
                  style={{
                    backgroundImage: `url(${MoneyImage})`,
                  }}
                >
                  <SniperReticle />
                </div>
                <h1 className="text-white w-[70%] text-shadow-black font-extrabold text-2xl md:text-3xl lg:text-4xl text-center ">
                  And <span className="italic font-bold">Always</span> Hit Your
                  Target.
                </h1>
              </div>
            </div>
            <div className="flex justify-center items-center h-200 w-full bg-cover ">
              <div className="">
                <h1 className="text-black "></h1>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
