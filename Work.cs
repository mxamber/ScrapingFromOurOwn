using System;

namespace ScrapingFromOurOwn
{
	public class Work
	{
		public String title;
		public String author;
//		String[] fandoms;
//		String[] ships;
//		String[] characters;
//		String[] freeform;
		public Language language = Language.UNDEFINED;
		public DateTime published;
		public DateTime updated;
		public int words;
		public int chapters;
		public int hits;
		public int kudos;
		public int comments;
		public int bookmarks;
		public String html;
		public String worktext = "";
		
		public Work(String html)
		{
			this.html = html;
		}
		
		public Work(String title, String author, int chapters = 1, int words = 0) {
			this.title = title;
			this.author = author;
			this.chapters = chapters;
			this.words = words;
		}
	}
}
