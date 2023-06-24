using EFilingWeb.Model;

namespace EFilingWeb.Resources;

public partial class Retainer {
    private static readonly Dictionary<string, string> specialNameChars = new() {
        { @"\", @"\textbackslash" }, // this must be first, else it will replace all other escaping backslashes
        { @"&", @"\&" },
        { @"$", @"\$" },
        { @"%", @"\%" },
        { @"#", @"\#" },
        { @"_", @"\_" },
        { @"{", @"\{" },
        { @"}", @"\}" },
        { @"~", @"\textasciitilde" },
        { @"^", @"\textasciicircum" }
    };

    private readonly RetainerAgreementData data;
    private readonly string courtName;
    private readonly string jurisdiction;
    private readonly string caseType;
    private readonly string expandedCaseNumber;
    private readonly string shortenedCaseNumber;
    private readonly string petitioner;
    private readonly string respondentName;
    private readonly string advocateName;
    private readonly string filingDate;

    public Retainer(RetainerAgreementData retainerAgreementData) {
        data = retainerAgreementData;
        courtName = formatStrToTex(data.CaseDetails.CourtName);
        jurisdiction = formatStrToTex(data.CaseDetails.Jurisdiction);
        caseType = formatStrToTex(data.CaseDetails.CaseType);
        expandedCaseNumber = formatStrToTex($"{data.CaseDetails.CaseNumberPart1} of {data.CaseDetails.CaseNumberPart2}");
        shortenedCaseNumber = formatStrToTex($"{data.CaseDetails.CaseNumberPart1} / {data.CaseDetails.CaseNumberPart2}");
        petitioner = formatStrToTex(data.CaseDetails.Petitioner);
        respondentName = formatStrToTex(data.Respondent.GetFullName());
        advocateName = formatStrToTex(data.Advocate.GetFullName());
        filingDate = formatStrToTex(data.CaseDetails.FilingDate.ToLongDateString());
    }

    private static string getAddress(Address address, bool isMultiLine) {
        string separator = isMultiLine ? @" \\" : " ";
        string line1 = $"{address.Line1},{separator}";
        string line2 = string.IsNullOrWhiteSpace(address.Line2) ? string.Empty : $"{address.Line2},{separator}";
        string line3 = string.IsNullOrWhiteSpace(address.Line3) ? string.Empty : $"{address.Line3},{separator}";
        string city = $"{address.City},{separator}";
        string district = string.IsNullOrWhiteSpace(address.District)
                              ? string.Empty
                              : $"{address.District},{separator}";
        string statePinCode = $"{address.State} -- {address.PinCode}";

        return $"{line1}{line2}{line3}{city}{district}{statePinCode}";
    }

    private static string getPersonDetails(Person person, bool isMultiLine) {
        string separator = isMultiLine ? @" \\" : " ";
        string name = $"{person.GetFullName()},{separator}";
        string org = $"{person.OrgTitle},{separator}";
        string address = getAddress(person.Address, isMultiLine);
        return $"{name}{org}{address}";
    }

    private static string formatStrToTex(string input) {
        foreach ((string character, string escape) in specialNameChars) {
            input = input.Replace(character, escape);
        }
        return input;
    }
}
