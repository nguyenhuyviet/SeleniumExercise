using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TShapedFoundation.Common
{
    class BasePage
    {
        public IWebDriver driver;
        private IWebElement element;
        IList<IWebElement> elements;
        private WebDriverWait explicitWait;
        private readonly long longtimeout = 90;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenUrl(String url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public IWebElement FindElement(By byLocator)
        {
            return driver.FindElement(byLocator);
        }

        public IList<IWebElement> FindElements(By byLocator)
        {
            return driver.FindElements(byLocator);
        }

        public void ClickToElement(By byLocator)
        {
            this.FindElement(byLocator).Click();
        }

        public void SendKeyToElement(By byLocator, string value)
        {
            element = this.FindElement(byLocator);
            element.Clear();
            element.SendKeys(value);
        }

        public String GetElementText(By byLocator)
        {
            return this.FindElement(byLocator).Text;
        }

        public String GetElementText(IWebElement element)
        {
            return element.Text;
        }

        public void SleepInMiliseconds(int time)
        {
            try
            {
                Thread.Sleep(time);
            }
            catch (ThreadInterruptedException e)
            {
                e.StackTrace.ToString();
            }
        }


        public void WaitForElementVisible(By byLocator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(byLocator));

        }

        public void WaitForElementExists(By byLocator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(byLocator));

        }

        public void WaitForElementInvisible(By byLocator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(byLocator));
        }
        public void WaitForElementClickable(By byLocator)
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(byLocator));

        }

        public void WaitForAlertPresent()
        {
            explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(longtimeout));
            explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

        }
    }
        
}
