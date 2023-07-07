using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EFilingWeb.Model;

public record Address([Required] [property: JsonPropertyName("line1")] string Line1,
                      [property: JsonPropertyName("line2")] string? Line2,
                      [property: JsonPropertyName("line3")] string? Line3,
                      [Required] [property: JsonPropertyName("city")] string City,
                      [property: JsonPropertyName("district")] string? District,
                      [Required] [property: JsonPropertyName("state")] string State,
                      [Required] [property: JsonPropertyName("pinCode")] string PinCode);
