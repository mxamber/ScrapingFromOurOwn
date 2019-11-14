using System;
using System.Text.RegularExpressions;
using ScrapingFromOurOwn;

namespace ScrapingFromOurOwn
{
	public class FandomQuery
	{
		public int minimum = -1;
		public int maximum = -1;
		public String tag;
		public String custom = "";
		public int results = 0;
		
		public FandomQuery(String tag)
		{
			this.tag = tag;
		}
		
		public bool beginQuery() {
			Regex work_regex = new Regex(@"([\d]+)\sWorks(\sfound|)\sin");
			Regex error_regex = new Regex("div[^\\>]*class=\"[\\w\\s]*errors");
			
			int works = 0;			// number of works: default 0 until a number can be found
			
			String url = "";
			
			if (this.minimum <0 && this.maximum > -1) {
				url = UrlGenerator.searchUrlMax(this.maximum, this.tag, this.custom);
			} else if (this.maximum < 0 && this.minimum > -1) {
				url = UrlGenerator.searchUrlMin(this.minimum, this.tag, this.custom);
			} else if (this.maximum > -1 && this.minimum > -1) {
				url = UrlGenerator.searchUrlMinMax(this.minimum, this.maximum, this.tag, this.custom);
			} else {
				url = UrlGenerator.searchUrl(this.tag, this.custom);
			}
			String raw = Scraper.scrape(url);							// scrape search results page 
			
			if(String.IsNullOrEmpty(raw) != true) {
				if(Int32.TryParse(work_regex.Match(raw).Groups[1].ToString().Trim(), out works) == false) {
					return false;
				}
				// if the response is not empty, regex for number and attempt to parse as int
				// if successful: parsed number will be stored in var works, replacing default 0
				// if unsuccessful, default 0 will remain
			} else {
				return false;
			}
			
			// either way, return number of works
			// successful scraping will return number
			// unsuccessful scraping will return 0
			this.results = works;
			
			return true;
		}
	}
}
