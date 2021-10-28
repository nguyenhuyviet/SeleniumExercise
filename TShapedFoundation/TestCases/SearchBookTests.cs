using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TShapedFoundation.Common;
using TShapedFoundation.PageObjects;

namespace TShapedFoundation
{
    [TestFixture]
    public class SearchBookTests
    {
        IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        BookStorePage bookStorePage;
        BookDetailPage bookDetailPage;
        LoggedInProfilePage loggedInProfilePage;

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
        }

        [TestCase("Design")]
        [TestCase("design")]
        [Test]
        public void SearchBooks(string searchText)
        {
            var expectedResult = new List<string> { "Designing Evolvable Web APIs with ASP.NET", "Learning JavaScript Design Patterns" };
            bookStorePage.GoToThisPage();
            var searchResults = bookStorePage.Search(searchText).Select(s => s.Text);

            //If the match items has the same items as the expectedResult and searchResults
            //Then the expectedResult has the same items as searchResults
            var matchItem = searchResults.Intersect(expectedResult);
            Assert.AreEqual(expectedResult.Count(), matchItem.Count());
            Assert.AreEqual(searchResults.Count(), matchItem.Count());
        }

        [TearDown]
        public void TearDown()
        {            
            driver.Quit();
        }
    }
}
