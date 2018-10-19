using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Automation.Core.PageObjects
{
	public class GoogleHomePage : PageObject<GoogleHomePage>
	{
		public GoogleHomePage(IWebDriver driver) : base(driver)
		{
			this.Url = "http://google.com";
			this.PageName = "Google";
		}

		public IWebElement SearchBar => this.driver.FindElement(By.Id("lst-ib"));
		public IWebElement SearchBar2 => this.driver.FindElement(By.CssSelector("input[id='lst-ib']"));
	}
}
