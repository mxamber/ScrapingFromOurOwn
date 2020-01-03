using System;

namespace ScrapingFromOurOwn
{
	public static class UrlGenerator
	{
		const String search_base = "https://archiveofourown.org/works?work_search[sort_column]=revised_at";
		const String search_min = "&work_search[words_from]=";
		const String search_max = "&work_search[words_to]=";
		const String search_tag = "&tag_id=";		
		
		public static String SanitiseChars(String input) {
			// replace special characters to mirror the way AO3 does
			// example: tag "Draco Malfoy/Harry Potter" is stored as "Draco Malfoy*s*Harry Potter"
			// example: tag "Draco Malfoy & Harry Potter" is stored as "Draco Malfoy *a* Harry Potter"
			input = input.Replace("&", "*a*");
			input = input.Replace("/", "*s*");
			return input;
		}
		
		
		public static String SearchUrl(String tag_name, String custom = "") {
			tag_name = SanitiseChars(tag_name);
			return search_base + search_tag + tag_name + custom;
		}
		
		public static String SearchUrlMin(int min_words, String tag_name, String custom = "") {
			tag_name = SanitiseChars(tag_name);
			return search_base + search_min + min_words + search_tag + tag_name + custom;
		}
		
		public static String SearchUrlMax(int max_words, String tag_name, String custom = "") {
			tag_name = SanitiseChars(tag_name);
			return search_base + search_max + max_words + search_tag + tag_name + custom;
		}
		
		public static String SearchUrlMinMax(int min_words, int max_words, String tag_name, String custom = "") {
			tag_name = SanitiseChars(tag_name);
			return search_base + search_min + min_words + search_max + max_words + search_tag + tag_name + custom;
		}
		
		
		
		public static String WorkUrl(int workId, int chapterId) {
			return "https://archiveofourown.org/works/" + workId.ToString() + "/chapter/" + chapterId.ToString();
		}
		
		public static String WorkUrl(int workId, bool series = false) {
			if(series == true) {
				return "https://archiveofourown.org/series/" + workId.ToString();
			} else {
				return "https://archiveofourown.org/works/" + workId.ToString();
			}
		}
		
	}
}
