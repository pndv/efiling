#region

using System.Text.Json.Serialization;

#endregion

namespace EFilingWeb.Model;

public record RetainerAgreementData([property: JsonPropertyName("advocate")]
                                    Person Advocate,
                                    [property: JsonPropertyName("respondent")]
                                    Person Respondent,
                                    [property: JsonPropertyName("caseDetails")]
                                    CaseDetails CaseDetails);
