import LoginForm from "./components/LoginForm";
import MoneyBg from "../../assets/background_image.jpeg";
import Logo from "/GrabbingMoneyColor_Icon.png";
import { motion } from "motion/react";

const fadeInForm = {
  hidden: { opacity: 0, x: 50 },
  visible: {
    opacity: 1,
    x: 0,
    transition: {
      duration: 0.7,
      ease: "easeOut",
    },
  },
};

export default function Login() {
  return (
    <div className=" w-screen h-[calc(100vh-57px)] bg-green-100 flex justify-center items-center overflow-x-hidden">
      <motion.div
        variants={fadeInForm}
        initial="hidden"
        animate="visible"
        className="flex flex-row capitalize justify-center w-full h-150"
      >
        <div
          className=" hidden lg:flex w-120 bg-cover  rounded-ss-4xl rounded-es-4xl "
          style={{ backgroundImage: `url(${MoneyBg})` }}
        >
          <div className="flex flex-col items-center  h-full w-full bg-green-700/90 rounded-l-4xl gap-10 p-10">
            <img src={Logo} alt="Logo" className="h-20 w-20" />
            <hr className="h-0.5 bg-white w-[90%]" />
            <p className="text-6xl text-white font-semibold text-center mb-10">
              Your money's waiting...
            </p>
            <p className="text-2xl italic text-white font-medium text-center">
              Login and let's manage it right
            </p>
          </div>
        </div>
        <div className="flex flex-col justify-center items-center w-120  bg-white/85 rounded-4xl lg:rounded-l-none  ">
          <h1 className="text-center text-6xl text-green-600 font-bold ">
            Login
          </h1>
          <LoginForm />
        </div>
      </motion.div>
    </div>
  );
}
