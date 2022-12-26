namespace EFiling.Model;

public class Data {
    public Data(Person advocate, Person respondent, CaseDetails caseDetails) {
        Advocate = advocate;
        Respondent = respondent;
        CaseDetails = caseDetails;
    }

    public Person Advocate { get; }
    public Person Respondent { get; }
    public CaseDetails CaseDetails { get; }
}
