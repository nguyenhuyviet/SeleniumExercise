using OpenQA.Selenium;
using System;
using TShapedFoundation.Common;

namespace TShapedFoundation.PageObjects
{
    class LoginPage : BasePage
    {

        private By usernameTextBox = By.Id("userName");
        private By passwordTextBox = By.Id("password");
        private By loginButton = By.Id("login");
        private By invalidLoginMessage = By.Id("name");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public HomePage LoginWithValidAccount(String username, String password)
        {
            SendKeyToElement(usernameTextBox, username);
            SendKeyToElement(passwordTextBox, password);
            ClickToElement(loginButton);
            return new HomePage(driver);
        }

        public void LoginWithInvalidAccount(String username, String password)
        {
            SendKeyToElement(usernameTextBox, username);
            SendKeyToElement(passwordTextBox, password);
            ClickToElement(loginButton);
        }
         public String GetInvalidLoginMessage()
        {
            WaitForElementVisible(invalidLoginMessage); 
            return GetElementText(invalidLoginMessage);
        }
    }
}
