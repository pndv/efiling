namespace EFilingWeb.Model;

public record Person(string LastName,
                     string? MiddleName,
                     string FirstName,
                     string OrgTitle,
                     Address Address) {
    
    public string GetFullName() {
        return $"{FirstName} {MiddleName ?? string.Empty} {LastName}";
    }
}
