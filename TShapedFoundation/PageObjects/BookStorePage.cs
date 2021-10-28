using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TShapedFoundation.Common;

namespace TShapedFoundation.PageObjects
{
    class BookStorePage : BasePage
    {
        private By byBooksWrapper = By.ClassName("books-wrapper");
        private By bySearchTextBox = By.Id("searchBox");
        private string relativePath = "/books";
        public BookStorePage(IWebDriver driver) : base(driver)
        {
        }

        public void GoToThisPage()
        {
            OpenUrl(Constant.BASE_URL + relativePath);
        }

        public string GetBookUrlInWrapperByName(string name)
        {
            WaitForElementVisible(byBooksWrapper);
            By byBook = By.XPath("//div[@class='books-wrapper']//a[text()='" + name + "']");
            IWebElement book = FindElement(byBook);
            return book.GetAttribute("href");
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
