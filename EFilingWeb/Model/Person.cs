using System.Text.Json.Serialization;

namespace EFilingWeb.Model;

public record Person([property: JsonPropertyName("lastName")] string LastName,
                     [property: JsonPropertyName("middleName")] string? MiddleName,
                     [property: JsonPropertyName("firstName")] string FirstName,
                     [property: JsonPropertyName("orgTitle")] string OrgTitle,
                     [property: JsonPropertyName("address")] Address Address) {
  public string getFullName() {
    return $"{FirstName} {MiddleName ?? string.Empty} {LastName}";
  }
}
