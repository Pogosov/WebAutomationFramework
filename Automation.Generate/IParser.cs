using System.Collections.Generic;
using System.Runtime.InteropServices;
using HtmlAgilityPack;

namespace Automation.Generate
{
	public interface IParser
	{
		List<Element> Parse(HtmlDocument html);
	}
}