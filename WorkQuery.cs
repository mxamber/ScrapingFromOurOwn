using System;
using System.Text.RegularExpressions;
using ScrapingFromOurOwn;

namespace ScrapingFromOurOwn
{
	public class WorkQuery
	{
		public WorkQuery()
		{
			
		}
		
		public static Regex regexNumericField(String name) {
			return new Regex("<dd class=\"" + name + "\">(?'" + name + "'\\d*)");
		}
		
		public static String matchProperty(Match match, String field) {
			return match.Groups[field].Success == true ? match.Groups[field].ToString().Trim() : "";
		}
		
		public static Work beginQuery(int id) {
			String url = UrlGenerator.workUrl(id);
			String raw = Scraper.scrape(url);
			
			
			if(String.IsNullOrEmpty(raw) == true) {
				throw new System.ArgumentException("Work could not be found!", id.ToString());
			}
			
			Work result = new Work();
			
			
			Regex title_regex = new Regex("<h2 class=\"[^\"]*title[^\"]*\">(?'title'[^\\<]*)");
			Regex author_regex = new Regex("<a[^>]*rel=\"author\"[^>]*>(?'author'[^<]*)");
			Regex publish_regex = new Regex("<dd class=\"published\">(?'publish'\\d\\d\\d\\d-\\d\\d-\\d\\d)");
			Regex update_regex = new Regex("<dd class=\"status\">(?'update'\\d\\d\\d\\d-\\d\\d-\\d\\d)");
			Regex bookmarks_regex = new Regex("<dd class=\"bookmarks\"><a[^>]*>(?'bookmarks'\\d*)");
			
			String title = "";
			String author = "";
			int chapters = 1;
			int words = 0;
			int comments = 0;
			int kudos = 0;
			int bookmarks = 0;
			int hits = 0;
			
			Match title_match = title_regex.Match(raw);
			Match author_match = author_regex.Match(raw);
			
			if(title_match.Groups["title"].Success == true) {
				title = title_match.Groups["title"].ToString().Trim();
			}
			if(author_match.Groups["author"].Success == true) {
				author = author_match.Groups["author"].ToString().Trim();
			}
			
			Match chapters_match = regexNumericField("chapters").Match(raw);
			Int32.TryParse(matchProperty(chapters_match, "chapters"), out chapters);
			
			Match words_match = regexNumericField("words").Match(raw);
			Int32.TryParse(matchProperty(words_match, "words"), out words);
			
			Match comments_match = regexNumericField("comments").Match(raw);
			Int32.TryParse(matchProperty(comments_match, "comments"), out comments);
			
			Match kudos_match = regexNumericField("kudos").Match(raw);
			Int32.TryParse(matchProperty(kudos_match, "kudos"), out kudos);
			
			Match bookmarks_match = bookmarks_regex.Match(raw);
			Int32.TryParse(matchProperty(bookmarks_match, "bookmarks"), out bookmarks);
			
			Match hits_match = regexNumericField("hits").Match(raw);
			Int32.TryParse(matchProperty(hits_match, "hits"), out hits);
			
			Match publish_match = publish_regex.Match(raw);
			DateTime published = new DateTime();
			if(publish_match.Groups["publish"].Success == true && DateTime.TryParse(publish_match.Groups["publish"].ToString(), out published) == false) {
				// checking first if the capture group exists. if not, condition will fail anyway.
				// then running int32 try parse, if successful, value will be stored regardless of if condition (out published)
				// if group exists but int try parse fails, published will be assigned january 1st, 1970 instead: a common failsafe value
				published = new DateTime(1970, 1, 1);
			}
			result.published = published;
			
			
			Match update_match = update_regex.Match(raw);
			DateTime updated = new DateTime();
			if(update_match.Groups["update"].Success == false || chapters == 1) {
				updated = published;
			} else if(update_match.Groups["update"].Success == true && DateTime.TryParse(update_match.Groups["update"].ToString(), out updated) == false) {
				// same procedure as above, except if no update date can be detected or there is only 1 chapter
				// the publish date will be used, since oneshots and fics with only 1 chapter yet don't have an update date
				published = new DateTime(1970, 1, 1);
			}
			result.updated = updated;
			
			result.title = title;
			result.author = author;
			result.chapters = chapters;
			result.words = words;
			result.comments = comments;
			result.kudos = kudos;
			result.bookmarks = bookmarks;
			result.hits = hits;
			
			
			return result;
		}
	}
}
