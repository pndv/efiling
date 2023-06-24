namespace EFilingWeb.Model;

public record CaseDetails(string CaseType,
                          string CaseNumberPart1,
                          string CaseNumberPart2,
                          DateTime FilingDate,
                          string CourtName,
                          string Petitioner,
                          string Jurisdiction) {
    public string getCaseInfo() {
        return $"{CaseType}:{CaseNumberPart1}/{CaseNumberPart2}";
    }

    public string getFilingDateLongFormat() {
        return FilingDate.ToLongDateString();
    }
}
