using System.Collections.Generic;
using System.Diagnostics;

namespace HPCSAApi.Models {
    [DebuggerDisplay("{Name},{City},{Province},{PostCode}")]
    public class FullDetailsResponse {
        public FullDetailsResponse() {
            Registrations = new List<FullDetailsRegistration>();
        }

        public string Name { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }

        public List<FullDetailsRegistration> Registrations { get; set; }
    }

    [DebuggerDisplay("{Number},{Status},{Register},{Board}")]
    public class FullDetailsRegistration {
        public FullDetailsRegistration() {
            Qualifications = new List<FullDetailsQualification>();
            Categories = new List<FullDetailsCategory>();
        }

        public string Number { get; set; }
        public string Status { get; set; }
        public string Register { get; set; }
        public string Board { get; set; }
        public List<FullDetailsQualification> Qualifications { get; set; }
        public List<FullDetailsCategory> Categories { get; set; }
    }

    [DebuggerDisplay("{Name},{DateObtained}")]
    public class FullDetailsQualification {
        public string Name { get; set; }
        public string DateObtained { get; set; }
    }

    [DebuggerDisplay("{PracticeType},{PracticeField},{Speciality},{SubSpeciality},{FromDate},{EndDate},{Status}")]
    public class FullDetailsCategory {
        public string PracticeType { get; set; }
        public string PracticeField { get; set; }
        public string Speciality { get; set; }
        public string SubSpeciality { get; set; }
        public string FromDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }
}
