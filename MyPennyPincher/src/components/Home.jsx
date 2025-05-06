import MoneyImage from "../assets/background_image.jpeg";
import Reticle from "../assets/Reticle.png";

export default function Home() {
  return (
    <>
      <div className=" w-full bg-scroll">
        <div
          className="flex flex-col items-center w-full capitalize bg-cover bg-center bg-fixed"
          style={{
            backgroundImage: `url(${MoneyImage})`,
          }}
        >
          <div className="backdrop-blur-md w-full h-full">
            <div className="flex justify-center h-180 w-full mt-20 ">
              <div className="flex flex-col w-[50vw] justify-center items-center p-10 ">
                <div className="bg-green-700/95 rounded-4xl p-25 border-2 border-white text-shadow shadow-black">
                  <h1 className="text-center text-white text-4xl md:text-6xl lg:text-8xl font-extrabold mb-10 text-stroke ">
                    Welcome to MyPennyPincher
                  </h1>
                  <p className="text-center text-xl italic text-white">
                    Where you can make cents off your chaos
                  </p>
                </div>
              </div>
            </div>
            <div
              className="w-100vw  h-full bg-cover bg-center bg-fixed "
              style={{
                backgroundImage: `url(${MoneyImage})`,
              }}
            >
              <div className="flex flex-col p-5 bg-green-700  w-full justify-start items-center  z-1">
                <h1 className="text-white text-shadow-black font-extrabold text-4xl text-center ">
                  Never lose sight of what{" "}
                  <span className="italic font-bold">Really</span> matters.
                </h1>
              </div>
              <div className="flex relative h-75 justify-center bg-transparent">
                <div className="flex justify-center bg-green-700 w-full h-full mask-hole z-20"></div>
                <img
                  src={Reticle}
                  alt="Reticle"
                  className="absolute h-75 z-20"
                />
              </div>
            </div>
          </div>
        </div>
        <div className="h-200"></div>
      </div>
    </>
  );
}
