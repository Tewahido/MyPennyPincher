import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";
import DashboardPage from "../pages/DashboardPage";

let homePage = new HomePage();
let loginPage = new LoginPage();
let dashboardPage = new DashboardPage();

let validLoginDetails;
let newIncomeData;
let newExpenseData;

describe("Transaction Related tests", () => {
  beforeEach(() => {
    cy.visit("http://localhost:5173");

    cy.fixture("validLoginDetails").then((details) => {
      validLoginDetails = details;
    });

    cy.fixture("NewIncomeData").then((data) => {
      newIncomeData = data;
    });

    cy.fixture("NewExpenseData").then((data) => {
      newExpenseData = data;
    });
  });

  it("Adding a New Income and Expense", () => {
    dashboardPage.navigate(validLoginDetails, homePage, loginPage);

    cy.get(dashboardPage.AddIncomeButton).click();
    cy.get(dashboardPage.SourceInput).type(newIncomeData.source);
    cy.get(dashboardPage.IncomeAmountInput).type(newIncomeData.amount);

    if (newIncomeData.monthly) {
      cy.get(dashboardPage.MonthlyInput).check();
    }

    cy.get(dashboardPage.AddIncomeSubmitBtn).click();

    cy.get(dashboardPage.ManageIncomeModal).should("not.be.visible");

    cy.get(dashboardPage.AddExpenseButton).click();
    cy.get(dashboardPage.DescriptionInput).type(newExpenseData.description);
    cy.get(dashboardPage.ExpenseAmountInput).type(newExpenseData.amount);

    if (newExpenseData.recurring) {
      cy.get(dashboardPage.RecurringInput).check();
    }

    cy.get(dashboardPage.AddExpenseSubmitBtn).click();

    cy.get(dashboardPage.ManageExpenseModal).should("not.be.visible");
  });
});
