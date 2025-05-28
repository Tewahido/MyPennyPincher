import { describe, test } from "vitest";
import { extractTokenExpiryTime } from "../utils/authUtils";

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

describe("authUtils tests", () => {
  const expiryTime = Math.floor(Date.now() / 1000) + 60 * 60;

  const token = generateMockJWT(expiryTime);

  test("return token expiry time", () => {
    const expected = new Date(expiryTime * 1000);

    const result = extractTokenExpiryTime(token);

    expect(result).toEqual(expected);
  });
});
