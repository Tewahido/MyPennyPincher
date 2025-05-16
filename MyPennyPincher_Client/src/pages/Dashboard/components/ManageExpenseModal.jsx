import { forwardRef, useRef, useImperativeHandle } from "react";
import { createPortal } from "react-dom";

const ManageExpenseModal = forwardRef(function ManageExpenseModal(
  { expense },
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
      className="fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 backdrop:bg-black/50 p-6 rounded-2xl bg-red-300 shadow-lg min-w-100"
    >
      <form onSubmit={handleAddIncome} className="flex flex-col">
        <h1 className=" text-4xl font-bold text-center my-5">
          {" "}
          {!expense ? "Add Expense" : "Edit Expense"}
        </h1>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold ">Description:</p>
          <input
            type="text"
            name="Source"
            className="h-full w-[70%] bg-red-100 text-red-900 rounded-lg mx-3 p-3 focus:outline-none"
            defaultValue={expense && expense.Description}
          />
        </label>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold ">Amount:</p>
          <input
            type="number"
            name="Amount"
            className="h-full w-[70%] bg-red-100 text-red-900  rounded-lg mx-3 p-3 focus:outline-none"
            defaultValue={expense ? expense.Amount : 0}
            min={0}
          />
        </label>
        <label className="flex h-10 p-1 items-center justify-start">
          <p className="font-bold ">Recurring :</p>
          <input
            type="checkbox"
            name="Recurring"
            className="mx-8"
            checked={expense && expense.Recurring}
          />
        </label>

        <div className="flex w-full justify-end gap-5 mt-10">
          <button
            onClick={handleClose}
            className="h-10 w-20 bg-red-100 text-red-900 font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-white"
          >
            Cancel
          </button>
          <button className="h-10  bg-red-800 text-red-100 font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-red-950">
            <p className="mx-2">{expense ? "Confirm" : "Add"}</p>
          </button>
        </div>
      </form>
    </dialog>,
    document.getElementById("modal")
  );
});

export default ManageExpenseModal;
