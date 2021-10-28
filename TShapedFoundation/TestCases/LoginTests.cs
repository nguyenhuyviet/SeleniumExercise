using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects;

namespace TShapedFoundation.TestCases
{
    [TestFixture]
    class LoginTests : WebDriverManagers
    {
        IWebDriver driver;
        HomePage homePage;
        LoginPage loginPage;
        

        [SetUp]
        public void Setup()
        {
            driver = CreateBrowserDriver("chrome");
            driver.Navigate().GoToUrl(Common.Constant.APP_URL);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void LoginWithValidUser()
        {
            String validUsername = Common.Constant.USERNAME;
            String validPassword = Common.Constant.PASSWORD;

            homePage = new HomePage(driver);
            homePage.CloseAdsPopup();
            loginPage = homePage.GoToLoginPage();
            homePage = loginPage.LoginWithValidAccount(validUsername, validPassword);         
          
            String headerText = homePage.GetUsernameLabelValue();
            Assert.AreEqual(headerText, validUsername, "Username is not displayed as expected.");
        }

        [Test]
        public void LoginWithInvalidUser()
        {
            String invalidUsername = "abc";
            String invalidPassword = "123456";

            homePage = new HomePage(driver);
            homePage.CloseAdsPopup();
            loginPage = homePage.GoToLoginPage();
            loginPage.LoginWithValidAccount(invalidUsername, invalidPassword);

            String invalidLoginMessage = loginPage.GetInvalidLoginMessage(); 
            Assert.AreEqual(invalidLoginMessage, "Invalid username or password!", "Warning message is not displayed as expected.");
        }

        
    }
}
