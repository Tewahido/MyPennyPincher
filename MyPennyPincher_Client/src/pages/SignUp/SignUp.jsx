import SignUpForm from "./components/SignUpForm";
import { motion } from "motion/react";
import Logo from "/GrabbingMoneyColor_Icon.png";
import SignUpBg from "../../assets/sign_up_background_image.jpg";

const fadeInForm = {
  hidden: { opacity: 0, x: -50 },
  visible: {
    opacity: 1,
    x: 0,
    transition: {
      duration: 0.7,
      ease: "easeOut",
    },
  },
};

export default function SignUp() {
  return (
    <div className="w-full h-[calc(100vh-57px)] bg-green-100 flex justify-center items-center ">
      <motion.div
        variants={fadeInForm}
        initial="hidden"
        animate="visible"
        className="flex flex-row capitalize justify-center w-full "
      >
        <div className="flex flex-col justify-center  w-120  bg-white/85 rounded-4xl lg:rounded-r-none">
          <h1 className="text-center text-6xl text-green-600 font-bold mt-10">
            Sign Up
          </h1>
          <SignUpForm />
        </div>
        <div
          className="hidden lg:flex flex-col justify-center items-center w-120 bg-cover  rounded-se-4xl rounded-ee-4xl"
          style={{ backgroundImage: `url(${SignUpBg})` }}
        >
          <div className=" flex flex-col items-center  h-full w-full bg-green-700/90 rounded-r-4xl  gap-10 p-10">
            <img src={Logo} alt="Logo" className="h-20 w-20" />
            <hr className="h-0.5 bg-white w-[90%]" />
            <p className="text-6xl text-white font-semibold text-center mb-10">
              Your financial future starts now...
            </p>
            <p className="text-2xl italic text-white font-medium text-center">
              Sign up and outsmart your spending
            </p>
          </div>
        </div>
      </motion.div>
    </div>
  );
}
