using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Linq;
using WebDriverManager.DriverConfigs.Impl;

namespace TShapedFoundation.Common
{
    class WebDriverManagers
    {
		private static IWebDriver _driver;

       	public static IWebDriver CreateBrowserDriver(String browserName)
		{
			if (browserName.SequenceEqual("firefox"))
			{
				new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
				_driver = new FirefoxDriver();
			}
			else if (browserName.SequenceEqual("chrome"))
			{

				new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
				_driver = new ChromeDriver();
			}
			else if (browserName.SequenceEqual("edge"))
			{
				new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
				_driver =  new EdgeDriver(); 
			}
			_driver.Manage().Window.Maximize();
			return _driver;
		}
		
		
	}
}
