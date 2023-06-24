namespace EFilingWeb.Model;

public record Address(string Line1,
                      string? Line2,
                      string? Line3,
                      string City,
                      string? District,
                      string State,
                      int PinCode);
