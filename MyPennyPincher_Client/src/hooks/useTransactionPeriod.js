import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { setMonth } from "../store/slices/monthSlice.js";
import { setFromMonth, setToMonth } from "../store/slices/monthRangeSlice.js";
import { getCurrentMonthAsString } from "../utils/dateUtils.js";

const TransactionPeriod = {
  MONTH: "month",
  MONTH_RANGE: "monthRange",
};

export default function useTransactionPeriod() {
  const dispatch = useDispatch();

  const month = useSelector((state) => state.month.month);
  const monthRange = useSelector((state) => state.monthRange);

  const [transactionPeriod, setTransactionPeriod] = useState(
    TransactionPeriod.MONTH
  );

  const [currentYear, currentMonth] = month.split("-");

  const [currentFromYear, currentFromMonth] = monthRange.fromMonth.split("-");
  const [currentToYear, currentToMonth] = monthRange.toMonth.split("-");

  const periodStart =
    transactionPeriod === TransactionPeriod.MONTH
      ? `${currentYear}-${currentMonth}-01`
      : `${currentFromYear}-${currentFromMonth}-01`;

  const lastDayOfMonth =
    transactionPeriod === TransactionPeriod.MONTH
      ? new Date(currentYear, currentMonth, 0).getDate()
      : new Date(currentToYear, currentToMonth, 0).getDate();

  const periodEnd =
    transactionPeriod === TransactionPeriod.MONTH
      ? `${currentYear}-${currentMonth.padStart(2, "0")}-${String(
          lastDayOfMonth
        ).padStart(2, "0")}`
      : `${currentToYear}-${currentToMonth.padStart(2, "0")}-${String(
          lastDayOfMonth
        ).padStart(2, "0")}`;

  function firstMonthPickerHandleChange(event) {
    let newMonth = event.target.value;

    if (!newMonth || newMonth === null) {
      newMonth = getCurrentMonthAsString();
    }

    if (transactionPeriod === TransactionPeriod.MONTH) {
      handleChangeMonth(newMonth);
    } else {
      handleFromMonthChange(newMonth);
    }
  }

  function handleChangeMonth(newMonth) {
    dispatch(setMonth(newMonth));
  }

  function handleTransactionPeriodChange(event) {
    setTransactionPeriod(event.target.value);
  }

  function handleFromMonthChange(newMonth) {
    dispatch(setFromMonth(newMonth));
  }

  function handleToMonthChange(event) {
    if (!event.target.value || event.target.value === null) {
      dispatch(setToMonth(getCurrentMonthAsString()));
      return;
    }
    dispatch(setToMonth(event.target.value));
  }

  return {
    periodStart,
    periodEnd,
    transactionPeriod,
    setTransactionPeriod,
    month,
    monthRange,
    currentFromMonth,
    currentFromYear,
    currentToMonth,
    currentToYear,
    currentMonth,
    currentYear,
    TransactionPeriod,
    firstMonthPickerHandleChange,
    handleChangeMonth,
    handleTransactionPeriodChange,
    handleFromMonthChange,
    handleToMonthChange,
  };
}
