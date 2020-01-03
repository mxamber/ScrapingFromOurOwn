# ScrapingFromOurOwn
Inofficial AO3 scraper / API formerly part of and now requirement for [mxamber/AO3scrape](https://github.com/mxamber/AO3scrape).

# Overview

<small><i><a href='http://ecotrust-canada.github.io/markdown-toc/'>Table of contents generated with markdown-toc</a></i></small>

  * [ScrapingFromOurOwn.FandomQuery](#scrapingfromourownfandomquery)
  * [ScrapingFromOurOwn.UrlGenerator](#scrapingfromourownurlgenerator)
  * [ScrapingFromOurOwn.WorkQuery](#scrapingfromourownworkquery)
  * [ScrapingFromOurOwn.Work](#scrapingfromourownwork)


## ScrapingFromOurOwn.FandomQuery

This class searches AO3 for works matching the specified search parameters and stores the amount of results found (**not** the actual works!)

### FandomQuery.FandomQuery(String tag, int minimum, int maximum, String custom)

Constructor. Creates a new FandomQuery instance with the **tag** property. Optional parameters: **minimum**, **maximum** and **custom**, allowing for specifying word count range and custom search parameters.

### FandomQuery.BeginQuery()

Executes the search. Word count range and custom search parameters have to be set before calling this method, either via the constructor method or manually. Returns `true`or `false` to indicate whether the query was successful (`false` in case of 404, empty response, or failing to extract the number from the received HTML code).

### Properties

* **int maximum:** minimum word count. default `-1`, will only affect search if changed.
* **int minimum:** maximum word count. default `-1`, will only affect search if changed.
* **String tag:** tag to be searched for works. can be fandom, character, fandom or canonical freeform.
* **String custom:** will be added at the end of search URL, allows custom search parameters.
* **int results:** default `0`, will change to actual resnumber of results upon calling `beginQuery`.

## ScrapingFromOurOwn.UrlGenerator

Generates the various URLs used internally, such as search result and work URLs for scraping. **All methods are static.**

### UrlGenerator.SanitiseChars(String input)

Replaces special characters to mirror AO3 doing the same for tags. Subject to further additions to ensure functioning search result URL generation.

### UrlGenerator.SearchURL(String tag_name, String custom)

Generates search results URL for all works in specified tag. Optional custom search parameters, default empty string. Returns string.

### UrlGenerator.SearchUrlMin(int min_words, String tag_name, String custom)

Same as **SearchUrl**, with minimum word count set. No maximum word count set.

### UrlGenerator.SearchUrlMax(int max_words, String tag_name, String custom)

Same as **SearchUrlMin** but with maximum word count. No minimum word count set.

### UrlGenerator.SearchUrlMinMax

Bringing together **SearchUrlMin** and **SearchUrlMax**, result URL has word count between min and max set. Optional custom parameters.

### UrlGenerator.WorkUrl(int workId, bool series)

URL to a particular work or series. Returns string. Parameter series is default false, if set true, link to series will be generated instead.

### UrlGenerator.WorkUrl(int workId, int chapterId)

Overload for **WorkUrl**. Takes chapter ID in addition, omits series parameter.

## ScrapingFromOurOwn.WorkQuery

Similar to **FandomQuery**. Scrapes the work page of a particular fic and stores result in the form of a `ScrapingFromOurOwn.Work` instance, if desired.

### WorkQuery.WorkQuery(int id)

Constructor. Sets query's ID property.

### WorkQuery.BeginQuery(int id)

Static method. Since no instance of **WorkQuery** is created, work ID is passed as parameter instead. Will throw a `System.ArgumentException` if work could not be found and return `ScrapingFromOurOwn.Work` instance otherwise. Currently retrieved work properties are:

* title
* author
* chapters
* words
* comments
* kudos
* bookmarks
* hits
* publication date
* update date
* language

All numeric values default to 0 if no value could be found (ex: no bookmarks yet). Default chapter amount is 1. Publication date defaults to Jan 1, 1970 if no date could be retrieved. Update date is = publication date if no update date could be retrieved (ex: only 1 chapter). If unforeseen errors occur, empty `Work` instance might be returned.

### WorkQuery.beginQuery()

Non-static overload for **BeginQuery()**. If `WorkQuery` instance was created, this method is available and uses internal ID created via constructor. Work result will not be returned and instead stored as `WorkQuery.result` property.

### Properties

* **int id:** work ID if an instance of the class was created.
* **ScrapingFromOurOwn.Work result:** found work if non-static **BeginQuery()** was used.

## ScrapingFromOurOwn.Work

Used to store properties of retrieved works. Currently stores the following properties:

* title
* author
* chapters
* words
* comments
* kudos
* bookmarks
* hits
* publication date
* update date
* language

The following properties might be retreived and stored in the future:

* ~~language~~ (added in 0.4.3)
* fandom tags
* character tags
* relationship tags
* freeform tags
## ScrapingFromOurOwn.Scraper.scrape(String url)

Static. Will return HTML code of any website as string. Literally just 10 lines of code that you could easily do yourself and just included because it looks fancy.

## ScrapingFromOurOwn.Exporter

Currently not in use. Intended for potential export of retrieved results as text file.
