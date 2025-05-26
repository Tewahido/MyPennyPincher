import GreenBg from "../../assets/Plain_Green_Background_Wallpaper.jpg";
import ServiceCard from "./components/ServiceCard.jsx";
import IncomeIcon from "../../assets/Incomes.png";
import ExpenseIcon from "../../assets/Expenses.png";
import ComingSoonIcon from "../../assets/ComingSoon.png";
import { motion } from "motion/react";
import { useRef } from "react";
import ContactUsForm from "./components/ContactUsForm.jsx";
import NewsletterForm from "./components/NewsletterForm.jsx";
import HeroBanner from "./components/HeroBanner.jsx";
import { useTokenChecker } from "../../hooks/useTokenChecker.js";

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
  const servicesRef = useRef();
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
            <section className="flex flex-col lg:flex-row justify-center lg:h-200 w-full mt-20 text-white">
              <HeroBanner servicesRef={servicesRef} />
            </section>
            <section className="p-3 my-10">
              <div className="px-5 lg:px-0">
                <motion.div
                  variants={fadeInSection}
                  initial="hidden"
                  whileInView="visible"
                  viewport={{ once: true, amount: 0.4 }}
                  className="flex  flex-col justify-center items-center rounded-2xl h-full w-full bg-green-950 py-5"
                >
                  <div
                    ref={servicesRef}
                    className="flex  h-25 mt-20 mb-30 lg:mb-15 lg:mt-10 items-center"
                  >
                    <h1 className="text-gray-200 capitalize text-5xl font-bold">
                      Our Services
                    </h1>
                  </div>
                  <div className="flex items-center justify-around flex-col lg:flex-row mb-25 h-300 lg:h-full w-full">
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
            </section>
            <section className=" p-3">
              <div className="flex flex-col h-full w-full rounded-2xl">
                <div className="flex justify-center w-full ">
                  <h1 className="text-white font-bold text-4xl lg:text-6xl m-1 lg:m-5">
                    Want to know more?
                  </h1>
                </div>
                <div className="flex lg:flex-row flex-col items-center lg:justify-center w-full h-full">
                  <div className="p-5 m-5 lg:m-10 ">
                    <ContactUsForm />
                  </div>
                  <div className="flex  items-center justify-center">
                    <h1 className="text-white font-bold text-3xl lg:text-6xl m-2 lg:m-5">
                      OR
                    </h1>
                  </div>
                  <div className="flex flex-col items-center w-100 p-5 m-5 lg:m-10">
                    <NewsletterForm />
                  </div>
                </div>
              </div>
            </section>
          </div>
        </div>
      </div>
    </>
  );
}
