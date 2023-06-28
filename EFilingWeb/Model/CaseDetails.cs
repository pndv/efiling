using System.Text.Json.Serialization;

namespace EFilingWeb.Model;

public record CaseDetails([property: JsonPropertyName("caseType")] string CaseType,
                          [property: JsonPropertyName("caseNumberPart1")] string CaseNumberPart1,
                          [property: JsonPropertyName("caseNumberPart2")] string CaseNumberPart2,
                          [property: JsonPropertyName("filingDate")] DateTime FilingDate,
                          [property: JsonPropertyName("courtName")] string CourtName,
                          [property: JsonPropertyName("petitioner")] string Petitioner,
                          [property: JsonPropertyName("jurisdiction")] string Jurisdiction) {
  public string getCaseInfo() {
    return $"{CaseType}:{CaseNumberPart1}/{CaseNumberPart2}";
  }
}
