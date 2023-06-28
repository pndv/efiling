using System.Text.Json.Serialization;

namespace EFilingWeb.Model;

public record Address([property: JsonPropertyName("line1")] string Line1,
                      [property: JsonPropertyName("line2")] string? Line2,
                      [property: JsonPropertyName("line3")] string? Line3,
                      [property: JsonPropertyName("city")] string City,
                      [property: JsonPropertyName("district")] string? District,
                      [property: JsonPropertyName("state")] string State,
                      [property: JsonPropertyName("pinCode")] string PinCode);
