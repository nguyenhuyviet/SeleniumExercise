using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TShapedFoundation.Common;

namespace TShapedFoundation.PageObjects
{
    class LoggedInProfilePage : BasePage
    {
        private By byProfileWrapper = By.ClassName("profile-wrapper");
        private By byOkModalButton = By.Id("closeSmallModal-ok");
        private By bySearchTextBox = By.Id("searchBox");
        private string relativePath = "/profile";

        public LoggedInProfilePage(IWebDriver driver) : base(driver)
        {
        }
        
        public void GoToThisPage()
        {
            OpenUrl(Constant.BASE_URL + relativePath);
        }

        public string GetBookUrlInWrapperByName(string name)
        {
            WaitForElementVisible(byProfileWrapper);
            By byBook = By.XPath("//div[@class='profile-wrapper']//a[text()='" + name + "']");
            IWebElement book = FindElement(byBook);
            return book.GetAttribute("href");
        }

        public void RemoveBook(string name)
        {
            WaitForElementVisible(byProfileWrapper);
            By byDeleteSpan = By.XPath("//div[@class='profile-wrapper']//a[text()='" + name + "']//ancestor::div[@class='rt-tr-group']//span[@title='Delete']");
            IWebElement spanDelete = FindElement(byDeleteSpan);
            spanDelete.Click();
            WaitForElementVisible(byOkModalButton);
            IWebElement okModalButton = FindElement(byOkModalButton);
            okModalButton.Click();
        }

        public IEnumerable<IWebElement> Search(string keyword)
        {
            WaitForElementVisible(bySearchTextBox);
            SendKeyToElement(bySearchTextBox, keyword);

            By bySearchResult = By.XPath("//span[starts-with(@id, 'see-book')]/a");
            var searchResult = FindElements(bySearchResult);
            return searchResult;
        }

    }
}
