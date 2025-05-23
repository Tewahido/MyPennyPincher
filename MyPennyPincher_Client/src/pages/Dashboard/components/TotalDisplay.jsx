export default function TotalDisplay({ title, total, textcolour }) {
  return (
    <div className="flex flex-col items-center h-full w-80 lg:w-[33%] bg-white  rounded-xl p-5 justify-evenly">
      <h1 className="text-lg sm:text-2xl xl:text-4xl font-bold  text-center">
        {title}
      </h1>
      <h1
        className={`text-5xl xl:text-7xl text-center ${textcolour} font-semibold truncate overflow-hidden w-full`}
      >
        R{total}
      </h1>
    </div>
  );
}
