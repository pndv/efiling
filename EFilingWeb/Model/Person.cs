using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EFilingWeb.Model;

public record Person([Required] [property: JsonPropertyName("lastName")] string LastName,
                     [property: JsonPropertyName("middleName")] string? MiddleName,
                     [Required] [property: JsonPropertyName("firstName")] string FirstName,
                     [property: JsonPropertyName("orgTitle")] string OrgTitle,
                     [Required] [property: JsonPropertyName("address")] Address Address) {
  public string getFullName() {
    return $"{FirstName} {MiddleName ?? string.Empty} {LastName}";
  }
}
