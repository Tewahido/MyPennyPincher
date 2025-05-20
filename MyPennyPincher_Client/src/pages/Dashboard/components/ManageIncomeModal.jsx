import { forwardRef, useRef, useImperativeHandle } from "react";
import { createPortal } from "react-dom";
import { useDispatch, useSelector } from "react-redux";
import { AddIncome, EditIncome } from "../../../services/incomeService";
import { addIncome, editIncome } from "../../../store/slices/incomeSlice.js";

const ManageIncomeModal = forwardRef(function ManageIncomeModal(
  { income },
  ref
) {
  const dispatch = useDispatch();
  const incomes = useSelector((state) => state.income.incomes);
  const user = useSelector((state) => state.user.user);
  const month = useSelector((state) => state.month.month) + "-01";
  const dialog = useRef(ref);

  useImperativeHandle(ref, () => {
    return {
      open() {
        dialog.current.showModal();
      },
    };
  });

  async function handleAddIncome(event) {
    event.preventDefault();
    const formData = new FormData(event.target);

    const isMonthly = formData.has("monthly");

    const currentIncome = {
      source: formData.get("source"),
      amount: formData.get("amount"),
      date: month,
      monthly: isMonthly,
      userId: user.userId,
    };
    const status = income
      ? await EditIncome(
          { ...currentIncome, incomeId: income.incomeId },
          user.token
        )
      : await AddIncome(currentIncome, user.token);

    if (status != 400 || status != 401) {
      income
        ? dispatch(editIncome({ ...currentIncome, incomeId: income.incomeId }))
        : dispatch(addIncome(currentIncome));
      handleClose();
    }
  }

  function handleClose(event) {
    event && event.preventDefault();
    dialog.current.close();
  }
  return createPortal(
    <dialog
      ref={dialog}
      className="fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 backdrop:bg-black/50 p-6 rounded-2xl bg-green-300 shadow-lg min-w-100"
    >
      <form onSubmit={handleAddIncome} className="flex flex-col">
        <h1 className=" text-4xl font-bold text-center my-5">
          {!income ? "Add Income" : "Edit Income"}
        </h1>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold ">Source:</p>
          <input
            type="text"
            name="source"
            className="h-full w-[75%] bg-green-100 text-green-900 rounded-lg mx-3 p-3 focus:outline-none"
            defaultValue={income && income.source}
          />
        </label>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold">Amount:</p>
          <input
            type="number"
            name="amount"
            className="h-full w-[75%] bg-green-100 text-green-900  rounded-lg mx-3 p-3 focus:outline-none"
            defaultValue={income ? income.amount : 0}
            min={0}
          />
        </label>
        <label className="flex h-10 p-1 items-center justify-start">
          <p className="font-bold">Monthly :</p>
          <input
            type="checkbox"
            name="monthly"
            className="mx-5"
            defaultChecked={income && income.monthly}
          />
        </label>

        <div className="flex w-full justify-end gap-5 mt-10">
          <button
            onClick={handleClose}
            className="h-10 w-20 bg-green-100 text-green-900 font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-white"
          >
            Cancel
          </button>
          <button
            type="submit"
            className="h-10 p-2 bg-green-800 text-green-100 font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-green-950"
          >
            <p className="mx-2">{income ? "Confirm" : "Add"}</p>
          </button>
        </div>
      </form>
    </dialog>,
    document.getElementById("modal")
  );
});

export default ManageIncomeModal;
