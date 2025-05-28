import { describe, test } from "vitest";
import { formatDate } from "../utils/dateUtils";

describe("formateDate tests", () => {
  const date = new Date("2025-05-28T13:45:12.345Z");

  test("return date in YYY-MM-DD format", () => {
    const expected = "2025-05-28";

    const result = formatDate(date);

    expect(result).toEqual(expected);
  });
});
