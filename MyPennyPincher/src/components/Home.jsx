import MoneyImage from "../assets/background_image.jpeg";

export default function Home() {
  return (
    <>
      <div className=" w-full bg-scroll">
        <div
          className="flex justify-center h-500 w-full capitalize bg-cover bg-center bg-fixed"
          style={{
            backgroundImage: `url(${MoneyImage})`,
          }}
        >
          <div className="flex justify-center h-150 w-full bg-green-800/50 mt-20">
            <div className="flex flex-col w-[50vw] justify-center items-center p-10 ">
              <h1 className=" text-center text-white md:text-2xl lg:text-5xl font-extrabold m-5 ">
                Welcome to MyPennyPincher
              </h1>
              <p className="text-center text-sm italic text-white">
                Where you can make cents off your chaos
              </p>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
