using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Automation.Generate;
using HtmlAgilityPack;
using mshtml;

namespace Automation.Web
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public HashSet<Element> ElementList { get; set; }
		public MainWindow()
		{
			InitializeComponent();
			ElementList = new HashSet<Element>();
		}

		private void NavigateToUrl_Click(object sender, RoutedEventArgs e)
		{
			var urlString = UrlTextBox.Text;
			var uri = new Uri(string.IsNullOrEmpty(urlString) ? "http://google.com" : UrlTextBox.Text);
			WebBrowser.Navigate(uri);
		}

		private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
		{
			ResetDocumentHandlers();
		}

		private void ResetDocumentHandlers()
		{
			var doc = (IHTMLDocument2)WebBrowser.Document;

			//setup click handler for web broswer
			HTMLDocumentEvents2_Event iEvent;
			iEvent = doc as HTMLDocumentEvents2_Event;
			iEvent.onmousedown += WebElementKeyPressHandler;

			UrlTextBox.Text = WebBrowser.Source.AbsoluteUri;
		}

		private void WebElementKeyPressHandler(IHTMLEventObj e)
		{
			if (SelectionEnabled.IsChecked != null && SelectionEnabled.IsChecked.Value)
			{
				IHTMLElement element = e.srcElement as mshtml.IHTMLElement;

				var el = Element.Create(element);

				if (ElementList.Contains(el))
				{
					//remove element
					ElementList.Remove(el);

					//remove red border
					SetupBorderStyle(element, "none");
				}
				else
				{
					//add to element list
					ElementList.Add(el);

					//setup red border style
					SetupBorderStyle(element, "solid", "red", "medium");
				}

				UpdateElementListBox();
			}
		}

		private void UpdateElementListBox()
		{
			ElementListBox.ItemsSource = ElementList;
			ElementListBox.Items.Refresh();
		}

		private void WebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
		{
			//cancel navigating if we are selecting objects
			if (SelectionEnabled.IsChecked != null && SelectionEnabled.IsChecked.Value)
			{
				e.Cancel = true;
			}
			else
			{
				ElementList.Clear();
				UpdateElementListBox();
			}
		}

		private void NavigateBack(object sender, RoutedEventArgs e)
		{
			if (WebBrowser.CanGoForward)
			{
				WebBrowser.GoForward();
			}
		}

		private void NavigateForward(object sender, RoutedEventArgs e)
		{
			if (WebBrowser.CanGoBack)
			{
				WebBrowser.GoBack();
			}
		}

		private void ResetHandlers_OnClick(object sender, RoutedEventArgs e)
		{
			ResetDocumentHandlers();
		}

		private void GeneratePageObject_OnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void AnalyzeElements_OnClick(object sender, RoutedEventArgs e)
		{
			IParser parser = new HtmlParser();
			string html = (WebBrowser.Document as HTMLDocument).documentElement.innerHTML;
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(html);

			var results = parser.Parse(doc);
			ElementList.Clear();

			foreach (var element in results)
			{
				ElementList.Add(element);
			}
			UpdateElementListBox();
		}

		private void SetupBorderStyle(IHTMLElement element, string border, string color = "red", string width = "medium")
		{
			var sg = new StyleGenerator();
			sg.ParseStyleString(element.style.cssText);
			sg.SetStyle("border-style", border);
			sg.SetStyle("border-color", color);
			sg.SetStyle("border-width", width);
			element.style.cssText = sg.GetStyleString();
		}

		private void ElementListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				var elements = e.AddedItems.Cast<Element>().ToList();
				var removed = e.RemovedItems.Cast<Element>().ToList();

				foreach (var element in elements)
				{
					if (element.HtmlElement == null)
					{
						var doc = (IHTMLDocument3)WebBrowser.Document;

						if (!string.IsNullOrWhiteSpace(element.Id))
						{
							element.HtmlElement = doc.getElementById(element.Id);
						}
						else
						{
							continue;
						}
					}

					SetupBorderStyle(element.HtmlElement, "solid", "yellow", "medium");
				}

				foreach (var element in removed)
				{
					if (element.HtmlElement == null)
					{
						continue;
					}

					SetupBorderStyle(element.HtmlElement, "none");
				}
			}
			catch
			{
				//Ignore
			}
		}
	}
}
