import {
  forwardRef,
  useRef,
  useImperativeHandle,
  useEffect,
  useState,
} from "react";
import { createPortal } from "react-dom";
import { useDispatch, useSelector } from "react-redux";
import { GetExpenseCategories } from "../../../services/expenseCategoryService";
import { logoutUser, addRecurringExpense } from "../../../util/util";
import { EditExpense, AddExpense } from "../../../services/expenseService";
import { editExpense, addExpense } from "../../../store/slices/expenseSlice";
import { useNavigate } from "react-router-dom";

const ManageExpenseModal = forwardRef(function ManageExpenseModal(
  { expense },
  ref
) {
  const user = useSelector((state) => state.user.user);
  const date = useSelector((state) => state.month.month) + "-01";

  const dispatch = useDispatch();
  const navigate = useNavigate();
  const dialog = useRef(ref);

  const [expenseCategories, setExpenseCategories] = useState([]);

  useEffect(() => {
    async function getExpenseCategories() {
      const response = await GetExpenseCategories(user.token);
      if (response?.status === 401) {
        logoutUser(dispatch, navigate);
      } else {
        const data = await response.json();
        setExpenseCategories(data);
      }
    }

    getExpenseCategories();
  }, []);

  useImperativeHandle(ref, () => {
    return {
      open() {
        dialog.current.showModal();
      },
    };
  });

  async function handleAddExpense(event) {
    event.preventDefault();
    const formData = new FormData(event.target);

    const isRecurring = formData.has("recurring");

    const currentExpense = {
      description: formData.get("description"),
      amount: +formData.get("amount"),
      date: date,
      monthly: isRecurring,
      expenseCategoryId: formData.get("expenseCategory"),
      userId: user.userId,
    };

    const status = expense
      ? await EditExpense(
          { ...currentExpense, expenseId: expense.expenseId },
          user.token
        )
      : await AddExpense(currentExpense, user.token);

    if (status != 400 || status != 401) {
      if (isRecurring) {
        addRecurringExpense(currentExpense, user.token);
      }
      expense
        ? dispatch(
            editExpense({ ...currentExpense, expenseId: expense.expenseId })
          )
        : dispatch(addExpense(currentExpense));
      handleClose();
      navigate("/dashboard");
    }
  }

  function handleClose(event) {
    event && event.preventDefault();

    dialog.current.close();
  }
  return createPortal(
    <dialog
      ref={dialog}
      className="fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 backdrop:bg-black/50 p-6 rounded-2xl bg-red-300 shadow-lg min-w-100"
    >
      <form onSubmit={handleAddExpense} className="flex flex-col">
        <h1 className=" text-4xl font-bold text-center my-5">
          {!expense ? "Add Expense" : "Edit Expense"}
        </h1>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold ">Description:</p>
          <input
            type="text"
            name="description"
            className="h-full w-[70%] bg-red-100 text-red-900 rounded-lg mx-3 p-3 focus:outline-none"
            defaultValue={expense && expense.description}
          />
        </label>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold ">Amount:</p>
          <input
            type="number"
            name="amount"
            className="h-full w-[70%] bg-red-100 text-red-900  rounded-lg mx-3 p-3 focus:outline-none"
            defaultValue={expense ? expense.amount : 0}
            min={0}
          />
        </label>
        <label className="flex h-10 p-1 items-center justify-between">
          <p className="font-bold ">Category:</p>

          <select
            name="expenseCategory"
            className=" h-full w-[70%] bg-red-100 text-red-900 rounded-lg mx-3 px-2 focus:outline-none"
          >
            {expenseCategories.map((category) => (
              <option
                key={category.expenseCategoryId}
                value={category.expenseCategoryId}
              >
                {category.name}
              </option>
            ))}
          </select>
        </label>
        <label className="flex h-10 p-1 items-center justify-start">
          <p className="font-bold ">Recurring :</p>
          <input
            type="checkbox"
            name="recurring"
            className="mx-8"
            defaultChecked={expense && expense.recurring}
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
