using System;
using System.IO;
using System.Net;

namespace ScrapingFromOurOwn
{
	public static class Scraper
	{		
		public static String Scrape(String url) {
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0" ;
			
			WebResponse response = null;
			
			try {
				response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
				String code = reader.ReadToEnd();
				reader.Close();
				response.Close();
				
				return code;
			} catch (Exception e) {
				
				Console.WriteLine("ERROR: Could not scrape HTML!");
				Console.WriteLine(e.ToString());
				
				return null;
			}
		}
	}
}
