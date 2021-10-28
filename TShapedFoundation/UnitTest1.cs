using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using WebDriverManager.DriverConfigs.Impl;

namespace TShapedFoundation
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;
        By byLoginButton = By.Id("login");
        By byUsernameTextbox = By.Id("userName");
        By byPasswordTextbox = By.Id("password");
        By byLoggedInUserLabel = By.Id("userName-value");
        By byCloseAdsButton = By.Id("close-fixedban");
        By byInvalidLoginMessage = By.Id("name");

        [SetUp]
        public void Setup()
        {
            //driver = new FirefoxDriver();
            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://demoqa.com/books");
        }

        [Test]
        [Category("smoke")]
        public void TC01_LoginWithValidUser()
        {

            String validUsername = "nhudinh";
            String validPassword = "Demo@123";

            IWebElement closeAdsButton = driver.FindElement(byCloseAdsButton);
            closeAdsButton.Click();
            IWebElement loginButton = driver.FindElement(byLoginButton);
            loginButton.Click();
            IWebElement usernameTextBox = driver.FindElement(byUsernameTextbox);
            usernameTextBox.Clear();
            usernameTextBox.SendKeys(validUsername);
            IWebElement passwordTextBox = driver.FindElement(byPasswordTextbox);
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(validPassword);
            loginButton = driver.FindElement(byLoginButton);
            loginButton.Click();

            WebDriverWait explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(byLoggedInUserLabel));
            IWebElement loggedInUserLabel = driver.FindElement(byLoggedInUserLabel);
            String headerText = loggedInUserLabel.Text;
            Assert.AreEqual(headerText, validUsername, "Username is not displayed as expected");

        }

        [TestCase("nhuding", "Demo@123")]
        [TestCase("nhudinh", "abc@123")]
        [TestCase("nhuding", "abc@123")]
        public void TC02_LoginWithInvalidUser(String invalidUsername, String invalidPassword)
        {
            IWebElement closeAdsButton = driver.FindElement(byCloseAdsButton);
            closeAdsButton.Click();
            IWebElement loginButton = driver.FindElement(byLoginButton);
            loginButton.Click();
            IWebElement usernameTextBox = driver.FindElement(byUsernameTextbox);
            usernameTextBox.Clear();
            usernameTextBox.SendKeys(invalidUsername);
            IWebElement passwordTextBox = driver.FindElement(byPasswordTextbox);
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(invalidPassword);
            loginButton = driver.FindElement(byLoginButton);
            loginButton.Click();

            WebDriverWait explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(byInvalidLoginMessage));
            IWebElement invalidLoginMessage = driver.FindElement(byInvalidLoginMessage);
            String warningMessage = invalidLoginMessage.Text;
            Assert.AreEqual(warningMessage, "Invalid username or password!", "Warning message is not displayed as expected");

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}