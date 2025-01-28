using System.Collections.Generic;

namespace HPCSAApi.Models {
    public class SearchResponse {
        public List<SearchResult> Results { get; set; }

        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalNum { get; set; }
        public int RowNum { get; set; }
        public int Truncated { get; set; }
        public int StartNum { get; set; }
        public int EndNum { get; set; }
        public int Initial { get; set; }
        public int SearchType { get; set; }
        public int Search { get; set; }
        public int ReportID { get; set; }
        public string SearchTerm { get; set; }
        public int Grouped { get; set; }
        public List<object> Exceptions { get; set; }
        public int Page { get; set; }
        public object Error { get; set; }
        public string Spelling { get; set; }
        public string NotDict { get; set; }
    }
}
