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
    public class DeleteBookTests
    {
        IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        BookStorePage bookStorePage;
        BookDetailPage bookDetailPage;
        LoggedInProfilePage loggedInProfilePage;
        string bookName = "Speaking JavaScript";

        [SetUp]
        public void SetUp()
        {
            driver = WebDriverManagers.CreateBrowserDriver("chrome");
            driver.Navigate().GoToUrl(Common.Constant.APP_URL); String validUsername = Common.Constant.USERNAME;
            String validPassword = Common.Constant.PASSWORD;

            homePage = new HomePage(driver);
            bookStorePage = new BookStorePage(driver);
            homePage.CloseAdsPopup();
            loginPage = homePage.GoToLoginPage();
            homePage = loginPage.LoginWithValidAccount(validUsername, validPassword);
            var bookDetailUrl = bookStorePage.GetBookUrlInWrapperByName(bookName);
            driver.Navigate().GoToUrl(bookDetailUrl);

            bookDetailPage = new BookDetailPage(driver);
            bookDetailPage.AddToYourCollection();
            bookDetailPage.WaitForAlertPresent();
            var alert = driver.SwitchTo().Alert();
            Assert.AreEqual(alert.Text, "Book added to your collection.");
            alert.Accept();
        }

        [Test]
        public void DeleteBookSuccessfully()
        {            
            loggedInProfilePage = new LoggedInProfilePage(driver);
            loggedInProfilePage.GoToThisPage();
            loggedInProfilePage.Search(bookName);
            var bookUrl = loggedInProfilePage.GetBookUrlInWrapperByName(bookName);
            Assert.IsFalse(string.IsNullOrWhiteSpace(bookUrl));
            loggedInProfilePage.RemoveBook(bookName);
        }

        [TearDown]
        public void TearDown()
        {            
            driver.Quit();
        }
    }
}
