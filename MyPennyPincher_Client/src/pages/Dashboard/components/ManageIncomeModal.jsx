import { forwardRef, useRef, useImperativeHandle } from "react";
import { createPortal } from "react-dom";

const ManageIncomeModal = forwardRef(function ManageIncomeModal(
  { income },
  ref
) {
  const dialog = useRef(ref);

  useImperativeHandle(ref, () => {
    return {
      open() {
        dialog.current.showModal();
      },
    };
  });

  function handleAddIncome() {}

  function handleClose(event) {
    event.preventDefault();
    dialog.current.close();
  }
  return createPortal(
    <dialog
      ref={dialog}
      className="fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 backdrop:bg-black/50 p-6 rounded-2xl bg-green-300 shadow-lg w-[25%]"
    >
      <form onSubmit={handleAddIncome} className="flex flex-col">
        <h1 className=" text-4xl font-bold text-center my-5">Add Income</h1>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold ">Source:</p>
          <input
            type="text"
            name="Source"
            className="h-full w-[75%] bg-green-100 text-green-900 rounded-lg mx-3 p-3 focus:outline-none"
          />
        </label>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold">Amount:</p>
          <input
            type="number"
            name="Amount"
            className="h-full w-[75%] bg-green-100 text-green-900  rounded-lg mx-3 p-3 focus:outline-none"
          />
        </label>
        <label className="flex h-10 p-1 items-center justify-start">
          <p className="font-bold">Monthly :</p>
          <input type="checkbox" name="Monthly" className="mx-5" />
        </label>

        <div className="flex w-full justify-end gap-5 mt-10">
          <button
            onClick={handleClose}
            className="h-10 w-20 bg-green-100 text-green-900 font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-white"
          >
            Cancel
          </button>
          <button className="h-10 w-15 bg-green-800 text-green-100 font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-green-950">
            Add
          </button>
        </div>
      </form>
    </dialog>,
    document.getElementById("modal")
  );
});

export default ManageIncomeModal;
