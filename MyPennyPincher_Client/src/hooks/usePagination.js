import { useState } from "react";

export default function usePagination(totalCount, limit, setOffset) {
  const [currentPage, setCurrentPage] = useState(0);

  const totalPages = Math.ceil(totalCount / limit);

  function handlePageChange(event) {
    const newOffset = event.selected * limit;
    setOffset(newOffset);
    setCurrentPage(event.selected);
  }

  return {
    currentPage,
    totalPages,
    handlePageChange,
  };
}
