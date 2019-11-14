using System;

namespace ScrapingFromOurOwn
{
	public class UrlGenerator
	{
		const String strbase_srch = "https://archiveofourown.org/works?work_search[sort_column]=revised_at";
		const String str1_srch = "&work_search[words_from]=";
		const String str2_srch = "&work_search[words_to]=";
		const String str3_srch = "&tag_id=";
		
		public UrlGenerator() {
			// empty			
		}
		
		
		public static String sanitiseChars(String input) {
			input = input.Replace("&", "*a*");
			return input;
		}
		
		
		public static String searchUrl(String tag_name, String custom = "") {
			tag_name = sanitiseChars(tag_name);
			return strbase_srch + str3_srch + tag_name + custom;
		}
		
		public static String searchUrlMin(int min_words, String tag_name, String custom = "") {
			tag_name = sanitiseChars(tag_name);
			return strbase_srch + str1_srch + min_words + str3_srch + tag_name + custom;
		}
		
		public static String searchUrlMax(int max_words, String tag_name, String custom = "") {
			tag_name = sanitiseChars(tag_name);
			return strbase_srch + str2_srch + max_words + str3_srch + tag_name + custom;
		}
		
		public static String searchUrlMinMax(int min_words, int max_words, String tag_name, String custom = "") {
			tag_name = sanitiseChars(tag_name);
			return strbase_srch + str1_srch + min_words + str2_srch + max_words + str3_srch + tag_name + custom;
		}
		
		
		
		public static String workUrl(int workId, int chapterId) {
			return "https://archiveofourown.org/works/" + workId.ToString() + "/chapter/" + chapterId.ToString();
		}
		
		public static String workUrl(int workId, bool series = false) {
			if(series == true) {
				return "https://archiveofourown.org/series/" + workId.ToString();
			} else {
				return "https://archiveofourown.org/works/" + workId.ToString();
			}
		}
		
	}
}
