import { forwardRef, useRef, useImperativeHandle } from "react";
import { createPortal } from "react-dom";
import { DeleteIncome } from "../../../services/incomeService";
import { useDispatch, useSelector } from "react-redux";
import { deleteIncome } from "../../../store/slices/incomeSlice";

const DeletConfirmationModal = forwardRef(function DeletConfirmationModal(
  { entry, type },
  ref
) {
  const token = useSelector((state) => state.user.user?.token);
  const dispatch = useDispatch();

  const dialog = useRef(ref);

  useImperativeHandle(ref, () => {
    return {
      open() {
        dialog.current.showModal();
      },
    };
  });

  function handleClose(event) {
    event && event.preventDefault();
    dialog.current.close();
  }

  async function handleDelete() {
    const status = type === "income" ? await DeleteIncome(entry, token) : null;

    if (status != 400 || status != 401) {
      type === "income" ? dispatch(deleteIncome(entry)) : null;
      handleClose();
    }
  }

  return createPortal(
    <dialog
      ref={dialog}
      className="fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 backdrop:bg-black/50 p-6 rounded-xl bg-white shadow-lg w-[25%]"
    >
      <div className="flex flex-col items-center text-center gap-5">
        <h1 className="text-4xl font-semibold">Are you sure?</h1>
        <p className="text-xl font-semibold ">
          You are about to delete this {type}:{" "}
          {entry && type === "income" ? entry.Source : entry.Description}
        </p>
      </div>
      <div className="flex w-full justify-end gap-5 mt-10">
        <button
          onClick={handleClose}
          className="h-10 w-20 bg-gray-500 text-white font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-gray-400 hover:text-gray-800 focus:outline-none"
        >
          Cancel
        </button>
        <button
          onClick={handleDelete}
          className="h-10 bg-red-700/85 text-black/85 font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-red-800 hover:text-white focus:outline-none"
        >
          <p className="mx-2"> Delete</p>
        </button>
      </div>
    </dialog>,
    document.getElementById("modal")
  );
});

export default DeletConfirmationModal;
