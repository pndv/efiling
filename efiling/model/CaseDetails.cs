using System;

namespace efiling.model {
    public class CaseDetails {
        public CaseDetails(string caseType,
                           string caseNumberPart1,
                           string caseNumberPart2,
                           DateTime filingDate,
                           string courtName,
                           string petitioner,
                           string jurisdiction) {
            CaseType = caseType;
            CaseNumberPart1 = caseNumberPart1;
            CaseNumberPart2 = caseNumberPart2;
            FilingDate = filingDate.Date.ToLongDateString();
            CourtName = courtName;
            Petitioner = petitioner;
            Jurisdiction = jurisdiction;
        }

        public string CaseType { get; }
        public string CaseNumberPart1 { get; }
        public string CaseNumberPart2 { get; }
        public string FilingDate { get; }
        public string CourtName { get; }
        public string Petitioner { get; }
        public string Jurisdiction { get; }
    }
}