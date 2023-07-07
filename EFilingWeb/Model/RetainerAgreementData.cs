#region

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace EFilingWeb.Model;

public record RetainerAgreementData([Required] [property: JsonPropertyName("advocate")]
                                    Person Advocate,
                                    [Required] [property: JsonPropertyName("respondent")]
                                    Person Respondent,
                                    [Required(ErrorMessage = "Case details are required")]
                                    [property: JsonPropertyName("caseDetails")]
                                    CaseDetails CaseDetails);
