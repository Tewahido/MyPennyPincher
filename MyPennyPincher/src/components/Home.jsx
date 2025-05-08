import MoneyImage from "../assets/background_image.jpeg";
import SniperReticle from "./SniperReticle.jsx";
import GreenBg from "../assets/Plain_Green_Background_Wallpaper.jpg";
import DiagonalLine from "./DiagonalLine.jsx";
import ServiceCard from "./ServiceCard.jsx";
import IncomeIcon from "../assets/Incomes.png";
import ExpenseIcon from "../assets/Expenses.png";
import ComingSoonIcon from "../assets/ComingSoon.png";
import { motion } from "motion/react";

const services = [
  {
    title: "Income Tracker",
    content: "Store all your incomes and plan your months accordingly",
    textColor: "text-green-600",
    icon: IncomeIcon,
  },
  {
    title: "Expense tracker",
    content:
      "Store all your expenses to keep track of where your money's going",
    textColor: "text-red-700",
    icon: ExpenseIcon,
  },
  {
    title: "Coming soon ...",
    content: "Look out for announcements on exciting new features!",
    textColor: "black",
    icon: ComingSoonIcon,
  },
];

const fadeInSection = {
  hidden: { opacity: 0, y: 50 },
  visible: {
    opacity: 1,
    y: 0,
    transition: {
      duration: 0.7,
      ease: "easeOut",
    },
  },
};

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
            <div className="flex flex-col lg:flex-row justify-center lg:h-200 w-full mt-20 text-white">
              <div className="flex flex-col w-[100%] justify-center items-center p-10 text-center ">
                <h1 className="  text-2xl md:text-4xl lg:text-6xl font-extrabold p-10  ">
                  Your all-in-one personal finance manager
                </h1>
                <hr className="flex h-0.5 w-[75%] mx-auto m-7.5 bg-white" />
                <p className=" text-lg font-bold w-[80%]">
                  Easily track your money <br /> going in or out <br /> without
                  a doubt
                </p>

                <div className="flex flex-row h-15 justify-center items-center w-full mt-10">
                  <button className="h-full w-35 bg-white rounded-lg cursor-pointer text-green-800 font-bold border-2 border-green-850 transition-all duration-200 hover:bg-green-800 hover: hover:border-white hover:text-white">
                    Get Started
                  </button>

                  <p className=" underline m-5 text-center cursor-pointer transition-all duration-150 hover:scale-105 hover:text-white">
                    View our services
                  </p>
                </div>
              </div>

              <div className=" h-[70%] w-100 justify-center items-center my-auto hidden lg:flex">
                <DiagonalLine color="white" />
              </div>
              <div className="flex flex-col w-[100%] items-center justify-center ">
                <div className="flex flex-col p-5 w-full justify-start items-center  z-1">
                  <h1 className=" w-[80%] font-extrabold text-2xl md:text-3xl lg:text-4xl text-center ">
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
                <h1 className=" w-[70%] text-shadow-black font-extrabold text-2xl md:text-3xl lg:text-4xl text-center ">
                  And <span className="italic font-bold">Always</span> Hit Your
                  Target.
                </h1>
              </div>
            </div>
            <div className="p-3">
              <motion.div
                variants={fadeInSection}
                initial="hidden"
                whileInView="visible"
                viewport={{ once: true, amount: 0.4 }}
                className="flex  flex-col justify-center items-center rounded-2xl h-full w-full bg-green-950  py-5 "
              >
                <div className="flex  h-25 mt-20 mb-30 lg:mb-15 lg:mt-10 items-center ">
                  <h1 className="text-gray-200 capitalize text-5xl font-bold ">
                    Our Services
                  </h1>
                </div>
                <div className="flex items-center justify-around flex-col lg:flex-row mb-25 h-300 lg:h-full w-full ">
                  {services.map((service, index) => {
                    return (
                      <ServiceCard
                        service={service}
                        textcolor={service.textColor}
                        key={index}
                      />
                    );
                  })}
                </div>
              </motion.div>
            </div>
            <div className="h-200 "></div>
          </div>
        </div>
      </div>
    </>
  );
}
