using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EFilingWeb.Model;

[DisplayName("Case Details")]
public record CaseDetails([Required] [property: JsonPropertyName("caseType")] string CaseType,
                          [Required] [property: JsonPropertyName("caseNumberPart1")] string CaseNumberPart1,
                          [Required] [property: JsonPropertyName("caseNumberPart2")] string CaseNumberPart2,
                          [Required] [property: JsonPropertyName("filingDate")] DateTime FilingDate,
                          [Required] [property: JsonPropertyName("courtName")] string CourtName,
                          [Required] [property: JsonPropertyName("petitioner")] string Petitioner,
                          [Required] [property: JsonPropertyName("jurisdiction")] string Jurisdiction) {
  public string getCaseInfo() {
    return $"{CaseType}:{CaseNumberPart1}/{CaseNumberPart2}";
  }
}
