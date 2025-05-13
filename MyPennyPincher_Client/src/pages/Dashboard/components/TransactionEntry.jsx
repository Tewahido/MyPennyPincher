import EditIcon from "../../../assets/edit_icon.png";
import DeleteIcon from "../../../assets/delete_icon.png";

export default function TransactionEntry({ type, entry, index }) {
  let entryTitle;
  let amountColour;
  let entryColour;

  if (type === "income") {
    entryTitle = entry.Source;
    amountColour = "text-green-700";
    entryColour = "bg-green-100";
  } else {
    entryTitle = entry.Description;
    amountColour = "text-red-700";
    entryColour = "bg-red-100";
  }

  return (
    <div
      className={`flex items-center justify-evenly ${entryColour} w-full rounded-xl p-5 `}
    >
      <h1 className="w-[25%] text-lg font-bold">{index}</h1>

      <h1 className="w-[40%] text-lg font-bold">{entryTitle}</h1>
      <h1 className={`w-[20%] ${amountColour} text-lg font-bold`}>
        R{entry.Amount}
      </h1>
      <div className=" w-[10%] h-[75%] flex justify-between items-center ">
        <div className="relative group">
          <img
            src={EditIcon}
            alt="Edit"
            className=" cursor-pointer transition duration-200 hover:scale-110"
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
  );
}
