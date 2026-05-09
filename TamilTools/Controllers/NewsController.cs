using Microsoft.AspNetCore.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;

namespace TamilTools.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _httpClient;

        public NewsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index(string category = "all")
        {
            var newsItems = new List<NewsItem>();

            var feeds = new List<string>();

            if (category == "all" || category == "general")
                feeds.Add("https://news.google.com/rss/search?q=tamil+news&hl=ta&gl=IN&ceid=IN:ta");
            if (category == "all" || category == "cinema")
                feeds.Add("https://news.google.com/rss/search?q=tamil+cinema&hl=ta&gl=IN&ceid=IN:ta");
            if (category == "all" || category == "sports")
                feeds.Add("https://news.google.com/rss/search?q=tamil+sports&hl=ta&gl=IN&ceid=IN:ta");
            if (category == "all" || category == "politics")
                feeds.Add("https://news.google.com/rss/search?q=tamil+politics&hl=ta&gl=IN&ceid=IN:ta");

            try
            {
                foreach (var feedUrl in feeds)
                {
                    var response = await _httpClient.GetStringAsync(feedUrl);
                    using var reader = XmlReader.Create(new StringReader(response));
                    var feed = SyndicationFeed.Load(reader);

                    foreach (var item in feed.Items.Take(10))
                    {
                        newsItems.Add(new NewsItem
                        {
                            Title = item.Title?.Text ?? "",
                            Link = item.Links?.FirstOrDefault()?.Uri?.ToString() ?? "#",
                            PublishedDate = item.PublishDate.LocalDateTime,
                            Summary = item.Summary?.Text ?? "",
                            Source = item.Authors?.FirstOrDefault()?.Name ?? "Google News"
                        });
                    }
                }

                newsItems = newsItems
                    .OrderByDescending(n => n.PublishedDate)
                    .Take(30)
                    .ToList();
            }
            catch
            {
                ViewBag.Error = "செய்திகள் கொண்டு வர முடியவில்லை!";
            }

            ViewBag.Category = category;
            ViewBag.NewsItems = newsItems;
            return View();
        }
    }

    public class NewsItem
    {
        public string Title { get; set; } = "";
        public string Link { get; set; } = "#";
        public DateTime PublishedDate { get; set; }
        public string Summary { get; set; } = "";
        public string Source { get; set; } = "";
    }
}