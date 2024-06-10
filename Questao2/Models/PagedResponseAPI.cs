using System.Text.Json.Serialization;

namespace Questao2.Models
{
    public class PagedResponseAPI<T> where T : class
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("data")]
        public List<T> Data { get; set; } = default!;
    }
}
