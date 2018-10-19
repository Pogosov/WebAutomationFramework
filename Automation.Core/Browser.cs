using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Automation.Core.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation.Core
{
	public class Browser
	{
		private IWebDriver driver;

		public Browser()
		{
			this.driver = new ChromeDriver();
		}

		//pages
		public GoogleHomePage Google => new GoogleHomePage(driver);

		public void Quit()
		{
			driver.Quit();
		}
	}
}
