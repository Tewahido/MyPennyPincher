export default function ServiceCard({ service, textcolor }) {
  return (
    <div
      className="group relative h-70 w-110 rounded-4xl hover:shadow-2xl z-10 hover:outline-2 outline-black/45 hover:bg-black/45 transition duration-50 bg-white outline-0"
      style={{
        transition: "0.1s ease-out",
      }}
    >
      <div className="flex relative flex-col justify-start items-center text-center z-10">
        <h1
          className={`text-4xl mt-10 mb-5 font-bold ${textcolor} text-stroke `}
        >
          {service.title}
        </h1>
        <img src={service.icon} alt="Icon" className="h-10 mb-5" />
        <p className="italic text-lg font-semibold text-black w-[75%] ">
          {service.content}
        </p>
      </div>
      <div
        className="absolute h-[98%] w-[98.5%] top-0.5 left-0.5  bg-white border-2 group-hover:border-gray-700 transition duration-100 border-white  rounded-4xl blur-xs z-5"
        style={{
          transition: "0.05s ease-out",
        }}
      ></div>
    </div>
  );
}
