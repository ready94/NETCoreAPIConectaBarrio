using System.Text.Json.Serialization;

namespace NETCoreAPIConectaBarrio.Helpers
{
    public class Source
    {
        [JsonPropertyName("id")]
        public int  id { get; set; }

        [JsonPropertyName("name")]
        public string? name { get; set; }
    }

    public class Article
    {
        [JsonPropertyName("articles")]
        public GoogleJSONDeserializer[] article { get; set; }
    }

    public class GoogleJSONDeserializer
    {
        [JsonPropertyName("source")]
        public Source source { get; set; }

        [JsonPropertyName("author")]
        public string? author { get; set; }

        [JsonPropertyName("title")]
        public string? title { get; set; }

        [JsonPropertyName("description")]
        public string? description { get; set; }

        [JsonPropertyName("url")]
        public string? url{ get; set; }

        [JsonPropertyName("urlToImage")]
        public string? urlToImage { get; set; }

        [JsonPropertyName("publishedAt")]
        public DateTimeOffset publishedAt { get; set; }

        [JsonPropertyName("content")]
        public string? content { get; set; }
    }
}
