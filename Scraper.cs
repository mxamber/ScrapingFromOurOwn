using System;
using System.IO;
using System.Net;

namespace ScrapingFromOurOwn
{
	public static class Scraper
	{		
		public static String scrape(String url) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			WebResponse response = null;
			
			try {
				response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
				String code = reader.ReadToEnd();
				reader.Close();
				response.Close();
				return code;
			} catch(WebException exception) {
//				Console.WriteLine(exception.ToString());
				return null;
			}
		}
	}
}
