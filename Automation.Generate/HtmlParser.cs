using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Automation.Generate
{
    public class HtmlParser : IParser
    {
	    private string[] Tags { get; set; }

	    public HtmlParser()
	    {
			Tags = new [] {"input","button","a","img","option","textarea","div","span","p",};
		}

	    public List<Element> Parse(HtmlDocument html)
	    {
		    var list = new HashSet<Element>();

			//todo: can we multi thread this process? (greedy approach)
			foreach (var tag in Tags)
		    {
				var nodes = html.DocumentNode.SelectNodes($"//{tag}");
				if(nodes == null) continue;
				foreach (var node in nodes)
				{
					list.Add(GetElementFromNode(node));
				}	
			}

		    return list.ToList();
	    }

	    private Element GetElementFromNode(HtmlNode node)
	    {
			var element = new Element
			{
				Name = node.GetAttributeValue("name", ""),
				Id = node.GetAttributeValue("id", ""),
				Type = node.GetAttributeValue("type", ""),
				Tag = node.Name,
				Html = node.OuterHtml,
				Class = node.GetAttributeValue("class", ""),
				Src = node.GetAttributeValue("src", ""),
				Href = node.GetAttributeValue("href", ""),
				Action = node.GetAttributeValue("action", ""),
				Alt = node.GetAttributeValue("alt", ""),
				Value = node.GetAttributeValue("value", ""),
				Role = node.GetAttributeValue("role", ""),
				AriaLabel = node.GetAttributeValue("aria-label", ""),
			};

		    return element;
	    }
	}
}
