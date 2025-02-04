using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HPCSAApi.Models;
using HPCSAApi.Utils;
using HtmlAgilityPack;

namespace HPCSAApi.Actions {
    public interface IFullDetails {
        Task<FullDetailsResponse> Get(string registrationNumberFromSearch);
    }
    public class FullDetails : IFullDetails {
        // GET https://hpcsaonline.custhelp.com/app/iregister_details/reg_number/{number}
        public async Task<FullDetailsResponse> Get(string registrationNumberFromSearch) {
            string url = "https://hpcsaonline.custhelp.com/app/iregister_details/reg_number/" + UrlEncoder.Default.Encode(registrationNumberFromSearch);
            var doc = await new HtmlWeb().LoadFromWebAsync(url);
            var document = doc.DocumentNode;

            HtmlNode containerNode = null;
            foreach (string xpath in new[] {
                "//div[@id='rn_iRegisterDetails_27']",
                "/html/body/div/div/div/div/div[@id='rn_iRegisterDetails_27']",
                "//p[@id='NAME']/../..",
                "/html/body/div[1]/div[1]/div[1]/div[3]/div[1]",
            }) {
                containerNode = document.SelectSingleNode(xpath);
                if (containerNode != null)
                    break;
            }
            if (containerNode == null)
                throw new ApplicationException("Cannot find results in document!").WithContent(document.OuterHtml);


            var rtn = new FullDetailsResponse {
                Name = containerNode.SelectSingleNode("//p[@id='NAME']").InnerText.Trim(),
                City = containerNode.SelectSingleNode("//p[@id='CITY']").InnerText,
                Province = containerNode.SelectSingleNode("//p[@id='PROVINCE']").InnerText,
                PostCode = containerNode.SelectSingleNode("//p[@id='POSTCODE']").InnerText
            };
            if (rtn.Name.Equals("iRegisterDetails_27", StringComparison.InvariantCultureIgnoreCase))
                throw new ApplicationException("No results found!").WithContent(document.OuterHtml);


            HtmlNode registrationNode = null;
            foreach (string xpath in new[] {
                "div[@class='registration']",
                "//div[@class='registration']",
                "div[5]",
            }) {
                registrationNode = containerNode.SelectSingleNode(xpath);
                if (containerNode != null)
                    break;
            }
            if (registrationNode != null) {
                FullDetailsRegistration currentRegistration = null;
                foreach (var node in registrationNode.ChildNodes) {
                    if (node.Name != "table" && node.Name != "div")
                        continue;

                    if (node.Name == "table") {
                        if (currentRegistration != null)
                            rtn.Registrations.Add(currentRegistration);

                        var dataRows = node.SelectSingleNode("tr[2]").SelectNodes("td");
                        currentRegistration = new FullDetailsRegistration {
                            Number = dataRows[0].InnerText.Trim(),
                            Status = dataRows[1].InnerText.Trim(),
                            Register = dataRows[2].InnerText.Trim(),
                            Board = dataRows[3].InnerText.Trim()
                        };
                    }
                    if (node.Name == "div" && node.Attributes["class"].Value == "qualification") {
                        if (currentRegistration == null)
                            currentRegistration = new FullDetailsRegistration();

                        var dataNode = node.SelectSingleNode("table");
                        foreach (var row in dataNode.SelectNodes("tr")) {
                            var rowCells = row.SelectNodes("td");
                            var qualification = new FullDetailsQualification() {
                                Name = rowCells[0].InnerText.Trim(),
                                DateObtained = rowCells[1].InnerText.Trim(),
                            };

                            if (qualification.Name.Equals("QUALIFICATION NAME", StringComparison.InvariantCultureIgnoreCase))
                                continue; // skip heading row
                            currentRegistration.Qualifications.Add(qualification);
                        }
                    }
                    if (node.Name == "div" && node.Attributes["class"].Value == "category") {
                        if (currentRegistration == null)
                            currentRegistration = new FullDetailsRegistration();

                        var dataNode = node.SelectSingleNode("table");
                        foreach (var row in dataNode.SelectNodes("tr")) {
                            var rowCells = row.SelectNodes("td");
                            var category = new FullDetailsCategory() {
                                PracticeType = rowCells[0].InnerText.Trim(),
                                PracticeField = rowCells[1].InnerText.Trim(),
                                Speciality = rowCells[2].InnerText.Trim(),
                                SubSpeciality = rowCells[3].InnerText.Trim(),
                                FromDate = rowCells[4].InnerText.Trim(),
                                EndDate = rowCells[5].InnerText.Trim(),
                                Status = rowCells[6].InnerText.Trim(),
                            };

                            if (category.PracticeType.Equals("PRACTICE TYPE", StringComparison.InvariantCultureIgnoreCase))
                                continue; // skip heading row
                            currentRegistration.Categories.Add(category);
                        }
                    }
                }

                if (currentRegistration != null)
                    rtn.Registrations.Add(currentRegistration);
            }

            return rtn;
        }
    }
}
