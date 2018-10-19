using System;
using mshtml;

namespace Automation.Generate
{
	public class Element : IEquatable<Element>
	{
		public string Name { get; set; }
		public string Id { get; set; }
		public string Type { get; set; }
		public string Tag { get; set; }
		public string Html { get; set; }
		public string Class { get; set; }
		public string Src { get; set; }
		public string Href { get; set; }
		public string Action { get; set; }
		public string Alt { get; set; }
		public string Value { get; set; }
		public string Role { get; set; }
		public string AriaLabel { get; set; }
		public string Selector => GenerateSelector();
		public IHTMLElement HtmlElement { get; set; }


		private string _selector = "";

		public static Element Create(IHTMLElement element)
		{
			var el = new Element
			{
				Id = element.id,
				Type = element.getAttribute("type")?.ToString(),
				Name = element.getAttribute("name")?.ToString(),
				Class = element.className,
				Src = element.getAttribute("src")?.ToString(),
				Href = element.getAttribute("href")?.ToString(),
				Action = element.getAttribute("action")?.ToString(),
				Alt = element.getAttribute("alt")?.ToString(),
				Value = element.getAttribute("value")?.ToString(),
				Role = element.getAttribute("role")?.ToString(),
				AriaLabel = element.getAttribute("aria-label")?.ToString(),
				Tag = element.tagName,
				Html = element.outerHTML,
				HtmlElement = element
			};

			return el;
		}

		public string GenerateSelector()
		{
			if (!string.IsNullOrWhiteSpace(_selector))
			{
				return _selector;		
			}
			
			switch (Tag.ToLower())
			{
				case "div":
				case "span":
				case "section":
				case "h1":
				case "h2":
				case "h3":
				case "h4":
				case "h5":
				case "h6":
				case "header":
				case "footer":
				case "ol":
				case "ul":
				case "li":
				case "p":
				case "label":
					if (!string.IsNullOrWhiteSpace(Id)) return $"[id='{Id}']";
					if (!string.IsNullOrWhiteSpace(Name)) return $"[name='{Name}']";
					if (!string.IsNullOrWhiteSpace(Class)) return $"[class='{Class}']";
					if (!string.IsNullOrWhiteSpace(AriaLabel)) return $"[aria-label='{AriaLabel}']";
					break;

				case "img":
					if (!string.IsNullOrWhiteSpace(Id)) return $"[id='{Id}']";
					if (!string.IsNullOrWhiteSpace(Name)) return $"[name='{Name}']";
					if (!string.IsNullOrWhiteSpace(Src)) return $"[src='{Src}']";
					if (!string.IsNullOrWhiteSpace(Class)) return $"[class='{Class}']";
					if (!string.IsNullOrWhiteSpace(AriaLabel)) return $"[aria-label='{AriaLabel}']";
					break;

				case "a":
					if (!string.IsNullOrWhiteSpace(Id)) return $"[id='{Id}']";
					if (!string.IsNullOrWhiteSpace(Name)) return $"[name='{Name}']";
					if (!string.IsNullOrWhiteSpace(Href)) return $"[href='{Href}']";
					if (!string.IsNullOrWhiteSpace(Class)) return $"[class='{Class}']";
					if (!string.IsNullOrWhiteSpace(AriaLabel)) return $"[aria-label='{AriaLabel}']";
					break;

				case "form":
					if (!string.IsNullOrWhiteSpace(Id)) return $"[id='{Id}']";
					if (!string.IsNullOrWhiteSpace(Name)) return $"[name='{Name}']";
					if (!string.IsNullOrWhiteSpace(Action)) return $"[action='{Action}']";
					if (!string.IsNullOrWhiteSpace(Class)) return $"[class='{Class}']";
					if (!string.IsNullOrWhiteSpace(AriaLabel)) return $"[aria-label='{AriaLabel}']";
					break;

				case "input":
				case "select":
				case "textarea":
					if (!string.IsNullOrWhiteSpace(Id)) return $"[id='{Id}']";
					if (!string.IsNullOrWhiteSpace(Name)) return $"[name='{Name}']";
					if (!string.IsNullOrWhiteSpace(Alt)) return $"[alt='{Alt}']";
					if (!string.IsNullOrWhiteSpace(Class)) return $"[class='{Class}']";
					if (!string.IsNullOrWhiteSpace(AriaLabel)) return $"[aria-label='{AriaLabel}']";
					break;

				case "option":
				case "button":
					if (!string.IsNullOrWhiteSpace(Id)) return $"[id='{Id}']";
					if (!string.IsNullOrWhiteSpace(Name)) return $"[name='{Name}']";
					if (!string.IsNullOrWhiteSpace(Value)) return $"[src='{Value}']";
					if (!string.IsNullOrWhiteSpace(Class)) return $"[class='{Class}']";
					if (!string.IsNullOrWhiteSpace(AriaLabel)) return $"[aria-label='{AriaLabel}']";
					break;

				default:
					if (!string.IsNullOrWhiteSpace(Id)) return $"[id='{Id}']";
					if (!string.IsNullOrWhiteSpace(Name)) return $"[name='{Name}']";
					if (!string.IsNullOrWhiteSpace(Alt)) return $"[alt='{Alt}']";
					if (!string.IsNullOrWhiteSpace(Class)) return $"[class='{Class}']";
					if (!string.IsNullOrWhiteSpace(AriaLabel)) return $"[aria-label='{AriaLabel}']";
					break;
			}

			return string.Empty;
		}

		public bool Equals(Element other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Html, other.Html);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Element) obj);
		}

		public override int GetHashCode()
		{
			return (Html != null ? Html.GetHashCode() : 0);
		}
	}
}



