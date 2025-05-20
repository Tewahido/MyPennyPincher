import EditIcon from "../../../assets/edit_icon.png";
import DeleteIcon from "../../../assets/delete_icon.png";
import ManageIncomeModal from "./ManageIncomeModal";
import ManageExpenseModal from "./ManageExpenseModal";
import { useRef } from "react";
import DeletConfirmationModal from "./DeleteConfirmationModal";

export default function TransactionEntry({ type, entry, index }) {
  const manageIncomeDialog = useRef();
  const manageExpenseDialog = useRef();
  const deleteConfirmationDialog = useRef();

  let entryTitle;
  let amountColour;
  let entryColour;

  if (type === "income") {
    entryTitle = entry.source;
    amountColour = "text-green-700";
    entryColour = "bg-green-100";
  } else {
    entryTitle = entry.description;
    amountColour = "text-red-700";
    entryColour = "bg-red-100";
  }

  function handleEditTransaction() {
    if (type === "income") {
      manageIncomeDialog.current.open();
      return;
    }

    manageExpenseDialog.current.open();
  }

  function handleDeleteTransaction() {
    deleteConfirmationDialog.current.open();
  }

  return (
    <>
      <DeletConfirmationModal
        ref={deleteConfirmationDialog}
        entry={entry}
        type={type}
      />
      <ManageIncomeModal ref={manageIncomeDialog} income={entry} />
      <ManageExpenseModal ref={manageExpenseDialog} expense={entry} />
      <div
        className={`flex items-center justify-evenly ${entryColour} w-full rounded-xl p-5 `}
      >
        <h1 className="w-[25%] text-lg font-bold">{index}</h1>

        <h1 className="w-[40%] text-lg font-bold">{entryTitle}</h1>
        <h1 className={`w-[20%] ${amountColour} text-lg font-bold`}>
          R{entry.amount}
        </h1>
        <div className=" w-[10%] h-[75%] flex justify-between items-center ">
          <div className="relative group">
            <img
              src={EditIcon}
              alt="Edit"
              className=" cursor-pointer transition duration-200 hover:scale-110"
              onClick={handleEditTransaction}
            />
            <span
              className="absolute bottom-full mb-1 left-1/2  
                   bg-gray-700 text-white text-xs px-2 py-1 rounded 
                   opacity-0 group-hover:opacity-100 transition-opacity duration-300"
            >
              Edit
            </span>
          </div>
          <div className="relative group">
            <img
              src={DeleteIcon}
              alt="Delete"
              className=" cursor-pointer transition duration-200 hover:scale-110"
              onClick={handleDeleteTransaction}
            />
            <span
              className="absolute bottom-full mb-1 left-1/2  
                   bg-gray-700 text-white text-xs px-2 py-1 rounded 
                   opacity-0 group-hover:opacity-100 transition-opacity duration-300"
            >
              Delete
            </span>
          </div>
        </div>
      </div>
    </>
  );
}
