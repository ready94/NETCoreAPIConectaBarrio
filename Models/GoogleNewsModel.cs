namespace NETCoreAPIConectaBarrio.Models
{
    public class GoogleNewsModel
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public DateTime PublishedAt { get; set; }
        public KeyValuePair<int, string> Source { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }

    }
    public class NewsResponse
    {
        public List<GoogleNewsModel> Articles { get; set; }
    }
}
