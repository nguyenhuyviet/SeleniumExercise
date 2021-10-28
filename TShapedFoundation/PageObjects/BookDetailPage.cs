using OpenQA.Selenium;
using System;
using TShapedFoundation.Common;

namespace TShapedFoundation.PageObjects
{
    class BookDetailPage : BasePage
    {
        private By byAddToYourCollectionButton = By.XPath("//button[@id='addNewRecordButton' and text()='Add To Your Collection']");
        private By byBackToBookStoreButton = By.XPath("//button[@id='addNewRecordButton' and text()='Back To Book Store']");
        public BookDetailPage(IWebDriver driver) : base(driver)
        {
        }

        public void AddToYourCollection()
        {
            ClickToElement(byAddToYourCollectionButton);
        }
  

    }
}
