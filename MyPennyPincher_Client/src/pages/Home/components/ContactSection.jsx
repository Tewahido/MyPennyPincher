import ContactUsForm from "./ContactUsForm.jsx";
import NewsletterForm from "./NewsletterForm.jsx";

export default function ContactSection() {
  return (
    <section className="p-3">
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
  );
}
