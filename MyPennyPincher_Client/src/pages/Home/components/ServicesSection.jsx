import { forwardRef } from "react";
import { servicesFade } from "../../../config/animationConfig.js";
import { services } from "../../../constants/services.js";
import ServiceCard from "./ServiceCard.jsx";
import { motion } from "motion/react";

const ServicesSection = forwardRef(function ServicesSection(
  { ...props },
  servicesRef
) {
  return (
    <section className="p-3 my-10">
      <div className="px-5 lg:px-0">
        <motion.div
          variants={servicesFade}
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
  );
});

export default ServicesSection;
