using System;
using System.Security.Cryptography.X509Certificates;
using Automation.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Test
{
	[TestClass]
	public class SimpleTest
	{
		[TestMethod]
		public void TestBrowserNavigation()
		{
			Browser browser = new Browser();
			browser.Google.Navigate();
			browser.Google.SetText(x => x.SearchBar2, "Hello");
			browser.Quit();
		}
	}
}
