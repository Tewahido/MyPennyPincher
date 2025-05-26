import GreenBg from "../../assets/Plain_Green_Background_Wallpaper.jpg";
import { useRef } from "react";
import HeroBanner from "./components/HeroBanner.jsx";
import ServicesSection from "./components/ServicesSection.jsx";
import ContactSection from "./components/ContactSection.jsx";

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
            <HeroBanner servicesRef={servicesRef} />
            <ServicesSection ref={servicesRef} />
            <ContactSection />
          </div>
        </div>
      </div>
    </>
  );
}
