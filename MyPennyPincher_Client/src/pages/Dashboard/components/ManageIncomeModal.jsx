import { forwardRef, useRef, useImperativeHandle, useState } from "react";
import { createPortal } from "react-dom";
import { useDispatch, useSelector } from "react-redux";
import { AddIncome, EditIncome } from "../../../services/incomeService";
import { addIncome, editIncome } from "../../../store/slices/incomeSlice.js";
import { useNavigate } from "react-router-dom";
import { addMonthlyIncome } from "../../../utils/recurringUtils.js";
import ErrorMessage from "../../../components/ErrorMessage.jsx";

const ManageIncomeModal = forwardRef(function ManageIncomeModal(
  { income },
  ref
) {
  const dialog = useRef(ref);

  const reloadIncomes = useSelector((state) => state.income.reloadIncomes);

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const user = useSelector((state) => state.user.user);
  const date = useSelector((state) => state.month.month) + "-01";

  const [invalidInput, setInvalidInput] = useState(false);

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

    if (
      formData.get("source") == "" ||
      formData.get("source") == null ||
      +formData.get("amount") == null
    ) {
      setInvalidInput(true);
      return;
    }

    const isMonthly = formData.has("monthly");

    const currentIncome = {
      source: formData.get("source"),
      amount: +formData.get("amount"),
      date: date,
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
      if (!income && isMonthly) {
        addMonthlyIncome(currentIncome, user.token);
      }
      income
        ? dispatch(editIncome({ ...currentIncome, incomeId: income.incomeId }))
        : dispatch(addIncome(currentIncome));
      handleClose();
      navigate("/dashboard");
    }
  }

  function handleClose(event) {
    event && event.preventDefault();

    const form = dialog.current.querySelector("form");

    if (form) {
      form.reset();
    }

    setInvalidInput(false);

    dialog.current.close();
  }

  return createPortal(
    <dialog
      ref={dialog}
      className="fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 backdrop:bg-black/50 p-6 rounded-lg bg-gray-300 shadow-lg min-w-100"
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
            className="h-full w-[60%] bg-gray-100 text-gray-900 rounded-lg mx-3 p-3 focus:outline-none"
            defaultValue={income && income.source}
          />
        </label>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold">Amount:</p>
          <input
            type="number"
            name="amount"
            className="h-full w-[60%] bg-gray-100 text-gray-900  rounded-lg mx-3 p-3 focus:outline-none"
            defaultValue={income ? income.amount : null}
            min={1}
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
        {invalidInput && (
          <div className="flex justify-center">
            <ErrorMessage text="Please fill all relevant fields" />
          </div>
        )}
        <div className="flex w-full justify-end gap-5 mt-10">
          <button
            onClick={handleClose}
            className="h-10 w-20 bg-gray-100 text-gray-900 font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-white"
          >
            Cancel
          </button>
          <button
            type="submit"
            className="h-10 p-2 bg-gray-800 text-gray-100 font-bold rounded-lg italic cursor-pointer transition duration-100 hover:bg-gray-950"
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
