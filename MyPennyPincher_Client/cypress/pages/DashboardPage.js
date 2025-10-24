class DashboardPage {
  LogoutLink = "#logoutLink";
  AddExpenseButton = "#addExpenseBtn";
  AddIncomeButton = "#addIncomeBtn";
  SourceInput = "#sourceInput";
  IncomeAmountInput = "#incomeAmountInput";
  MonthlyInput = "#monthlyInput";
  AddIncomeSubmitBtn = "#submitIncomeBtn";
  AddExpenseButton = "#addExpenseBtn";
  DescriptionInput = "#descriptionInput";
  ExpenseAmountInput = "#expenseAmountInput";
  AddExpenseSubmitBtn = "#submitExpenseBtn";
  ManageIncomeModal = "#manageIncomeModal";
  ManageExpenseModal = "#manageExpenseModal";

  navigate(validLoginDetails, homePage, loginPage) {
    cy.get(homePage.LoginLink).click();

    cy.get(loginPage.EmailInput).type(validLoginDetails.email);
    cy.get(loginPage.PasswordInput).type(validLoginDetails.password);
    cy.get(loginPage.SubmitButton).click();
  }
}
export default DashboardPage;
