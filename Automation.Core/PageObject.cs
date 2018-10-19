using System;
using OpenQA.Selenium;

namespace Automation.Core
{
	public abstract class PageObject<T> where T : class
	{
		protected IWebDriver driver;
		protected T Caller;

		public string Url { get; set; }
		public string PageName { get; set; }

		protected PageObject(IWebDriver driver)
		{
			this.driver = driver;
			Caller = this as T;
		}

		public void Navigate()
		{
			driver.Url = Url;
		}

		public void SetText(Func<T, IWebElement> selector, string text)
		{
			var el = selector(Caller);
			el.Click();
			el.Clear();
			el.SendKeys(text);
		}

	}
}
