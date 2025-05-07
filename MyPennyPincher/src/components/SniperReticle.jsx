import GreenBg from "../assets/Plain_Green_Background_Wallpaper.jpg";
import Reticle from "../assets/Scope.png";

export default function SniperReticle({ maskBg }) {
  return (
    <div className="flex relative h-75 justify-center bg-transparent rounded-full">
      <div className="flex absolute justify-center w-full h-full mask-hole z-10"></div>
      <img
        src={Reticle}
        alt="Reticle"
        className="absolute h-47 md:h-67 lg:h-77  z-20 -translate-y-1"
      />
    </div>
  );
}
