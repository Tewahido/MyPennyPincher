import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";
import DashboardPage from "../pages/DashboardPage";

let homePage = new HomePage();
let loginPage = new LoginPage();
let dashboardPage = new DashboardPage();

let validLoginDetails;

describe("Authentication Tests", () => {
  beforeEach(() => {
    cy.visit("http://localhost:5173");

    cy.fixture("ValidLoginDetails").then((details) => {
      validLoginDetails = details;
    });
  });

  it("Attempting Successful Login", () => {
    cy.get(homePage.LoginLink).click();

    cy.get(loginPage.EmailInput).type(validLoginDetails.email);
    cy.get(loginPage.PasswordInput).type(validLoginDetails.password);
    cy.get(loginPage.SubmitButton).click();

    cy.get(dashboardPage.LogoutLink).should("exist");
  });

  it("Attempting Logout", () => {
    cy.get(homePage.LoginLink).click();

    cy.get(loginPage.EmailInput).type(validLoginDetails.email);
    cy.get(loginPage.PasswordInput).type(validLoginDetails.password);
    cy.get(loginPage.SubmitButton).click();

    cy.get(dashboardPage.LogoutLink).click();

    cy.get(loginPage.LoginForm).should("exist");
  });
});
