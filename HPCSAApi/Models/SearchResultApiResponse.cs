using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HPCSAApi.Models {
    public class SearchResultApiResponse {
        [JsonPropertyName("data")]
        public List<List<System.Text.Json.JsonElement>>? Data { get; set; }
        [JsonPropertyName("headers")]
        public List<Header>? Headers { get; set; }
        public class Header {
            [JsonPropertyName("heading")]
            public string? Heading { get; set; }
            [JsonPropertyName("width")]
            public object? Width { get; set; }
            [JsonPropertyName("data_type")]
            public int DataType { get; set; }
            [JsonPropertyName("col_id")]
            public int ColId { get; set; }
            [JsonPropertyName("order")]
            public int Order { get; set; }
            [JsonPropertyName("col_definition")]
            public string? ColDefinition { get; set; }
            [JsonPropertyName("visible")]
            public bool Visible { get; set; }
        }
        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
        [JsonPropertyName("total_num")]
        public int TotalNum { get; set; }
        [JsonPropertyName("row_num")]
        public int RowNum { get; set; }
        [JsonPropertyName("truncated")]
        public int Truncated { get; set; }
        [JsonPropertyName("start_num")]
        public int StartNum { get; set; }
        [JsonPropertyName("end_num")]
        public int EndNum { get; set; }
        [JsonPropertyName("initial")]
        public int Initial { get; set; }
        [JsonPropertyName("search_type")]
        public int SearchType { get; set; }
        [JsonPropertyName("search")]
        public int Search { get; set; }
        [JsonPropertyName("report_id")]
        public int ReportID { get; set; }
        [JsonPropertyName("search_term")]
        public string? SearchTerm { get; set; }
        [JsonPropertyName("grouped")]
        public int Grouped { get; set; }
        [JsonPropertyName("exceptions")]
        public List<object>? Exceptions { get; set; }
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("error")]
        public object? Error { get; set; }
        [JsonPropertyName("spelling")]
        public string? Spelling { get; set; }
        [JsonPropertyName("not_dict")]
        public string? NotDict { get; set; }
    }
}
