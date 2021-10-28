using OpenQA.Selenium;
using System;
using TShapedFoundation.Common;

namespace TShapedFoundation.PageObjects
{
    class HomePage : BasePage
    {

        private By loginButton = By.Id("login");
        private By usernameLabel = By.Id("userName-value");
        private By closeAdsArrowButton = By.Id("close-fixedban");

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public LoginPage GoToLoginPage()
        {
            ClickToElement(loginButton);
            return new LoginPage(driver);
        }

        public String GetUsernameLabelValue()
        {
            WaitForElementVisible(usernameLabel);
            return GetElementText(usernameLabel);
        }

        public void CloseAdsPopup()
        {
            ClickToElement(closeAdsArrowButton);
        }

    }
}
