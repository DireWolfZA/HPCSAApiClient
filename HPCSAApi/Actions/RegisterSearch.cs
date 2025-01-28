using System.Linq;
using System.Threading.Tasks;
using HPCSAApi.Models;
using HPCSAApi.Utils;
using RestSharp;

namespace HPCSAApi.Actions {
    public interface IRegisterSearch {
        Task<SearchResponse> Search(string registrationNumber, string firstname = null, string surname = null, string city = null, string postcode = null, string register = null, string category = null);
    }
    public class RegisterSearch : IRegisterSearch {
        private readonly RestClient client;
        public RegisterSearch(RestClient client) {
            this.client = client;
        }

        // curl 'https://hpcsaonline.custhelp.com/cc/ReportController/getDataFromRnow' -X POST -H 'Content-Type: application/x-www-form-urlencoded; charset=UTF-8' --data-raw 'regNumber={number}&firstName=&surName=&city=&postalCode=&register=&category='
        public async Task<SearchResponse> Search(string registrationNumber, string firstname = null, string surname = null, string city = null, string postcode = null, string register = null, string category = null) {
            var request = new RestRequest("cc/ReportController/getDataFromRnow", Method.Post)
                .AddParameter("regNumber", registrationNumber)
                .AddParameter("firstName", firstname)
                .AddParameter("surName", surname)
                .AddParameter("city", city)
                .AddParameter("postalCode", postcode)
                .AddParameter("register", register)
                .AddParameter("category", category);

            var response = RestResponseHandler.Handle(await client.ExecuteAsync<SearchResultApiResponse>(request, Method.Post));

            var rtn = new SearchResponse() {
                Results = new System.Collections.Generic.List<SearchResult>(),
                PerPage = response.PerPage,
                TotalPages = response.TotalPages,
                TotalNum = response.TotalNum,
                RowNum = response.RowNum,
                Truncated = response.Truncated,
                StartNum = response.StartNum,
                EndNum = response.EndNum,
                Initial = response.Initial,
                SearchType = response.SearchType,
                Search = response.Search,
                ReportID = response.ReportID,
                SearchTerm = response.SearchTerm,
                Grouped = response.Grouped,
                Exceptions = response.Exceptions,
                Page = response.Page,
                Error = response.Error,
                Spelling = response.Spelling,
                NotDict = response.NotDict,
            };

            var headers = response.Headers.ToDictionary(h => h.Heading);
            var titleOrder = headers["Title"].Order;
            var surnameOrder = headers["Surname"].Order;
            var firstnameOrder = headers["Fullname"].Order;
            var registrationNumberOrder = headers["Registration"].Order;
            var cityOrder = headers["City"].Order;
            var postalCodeOrder = headers["Postal Code"].Order;
            var categoryOrder = headers["Category"].Order;
            var statusOrder = headers["Status"].Order;

            foreach (var result in response.Data)
                rtn.Results.Add(new SearchResult() {
                    Title = result[titleOrder].GetString(),
                    Surname = result[surnameOrder].GetString(),
                    Firstname = result[firstnameOrder].GetString(),
                    RegistrationNumber = result[registrationNumberOrder].GetString(),
                    City = result[cityOrder].GetString(),
                    PostalCode = result[postalCodeOrder].GetString(),
                    Category = result[categoryOrder].GetString(),
                    Status = result[statusOrder].GetString(),
                });

            return rtn;
        }
    }
}
