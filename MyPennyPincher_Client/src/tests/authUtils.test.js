import { describe, test } from "vitest";
import { extractTokenExpiryTime, isValidPassword } from "../utils/authUtils";

function generateMockJWT(expiryTime) {
  const jwt = require("jsonwebtoken");

  const payload = {
    sub: "user123",
    name: "Test User",
    exp: expiryTime,
  };

  const secret = "aeth4qnadetar4qtnwsrt1";

  return jwt.sign(payload, secret);
}

describe("token expiry time tests", () => {
  const expiryTime = Math.floor(Date.now() / 1000) + 60 * 60;

  const token = generateMockJWT(expiryTime);

  test("return token expiry time", () => {
    const expected = new Date(expiryTime * 1000);

    const result = extractTokenExpiryTime(token);

    expect(result).toEqual(expected);
  });
});

describe("password validity check tests", () => {
  const validPassword = "Abc!23you&me";
  const invalidPassword = "notValid";

  test("return true with valid password", () => {
    const expected = true;

    const result = isValidPassword(validPassword);

    expect(result).toEqual(expected);
  });

  test("return false with valid password", () => {
    const expected = false;

    const result = isValidPassword(invalidPassword);

    expect(result).toEqual(expected);
  });
});
