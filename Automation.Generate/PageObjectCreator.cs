using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Generate
{
	public class PageObjectCreator
	{
		public string CreateElementsById(List<Element> elements)
		{
			StringBuilder sb = new StringBuilder();

			foreach (Element el in elements)
			{
				sb.AppendLine($"		public IWebElement {el.Name} => this.driver.FindElement(By.Id(\"{el.Id}\"));");
			}

			return sb.ToString();
		}
	}
}
