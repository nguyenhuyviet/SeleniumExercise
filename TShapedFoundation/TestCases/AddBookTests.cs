using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects;

namespace TShapedFoundation
{
    [TestFixture]
    public class AddBookTests
    {
        IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        BookStorePage bookStorePage;
        BookDetailPage bookDetailPage;
        LoggedInProfilePage loggedInProfilePage;
        string bookName = "Git Pocket Guide";

        [SetUp]
        public void SetUp()
        {
            driver = WebDriverManagers.CreateBrowserDriver("chrome");
            driver.Navigate().GoToUrl(Common.Constant.APP_URL);
        }

        [Test]
        public void AddBookToCollection()
        {
            String validUsername = Common.Constant.USERNAME;
            String validPassword = Common.Constant.PASSWORD;

            homePage = new HomePage(driver);
            bookStorePage = new BookStorePage(driver);
            homePage.CloseAdsPopup();
            loginPage = homePage.GoToLoginPage();
            homePage = loginPage.LoginWithValidAccount(validUsername, validPassword);
            var bookDetailUrl = bookStorePage.GetBookUrlInWrapperByName("Git Pocket Guide");
            driver.Navigate().GoToUrl(bookDetailUrl);

            bookDetailPage = new BookDetailPage(driver);
            bookDetailPage.AddToYourCollection();
            bookDetailPage.WaitForAlertPresent();
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual(alert.Text, "Book added to your collection.");
            alert.Accept();
            loggedInProfilePage = new LoggedInProfilePage(driver);
            loggedInProfilePage.GoToThisPage();
            var bookUrl = loggedInProfilePage.GetBookUrlInWrapperByName(bookName);
            Assert.IsFalse(string.IsNullOrWhiteSpace(bookUrl));
        }

        [TearDown]
        public void TearDown()
        {
            loggedInProfilePage.RemoveBook(bookName);
            driver.Quit();
        }
    }
}
