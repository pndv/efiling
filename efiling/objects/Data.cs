using System;
using System.Collections.Generic;
using System.Text;

namespace efiling
{
    class Data
    {
        public Person Advocate { get; set; }
        public Person Respondent { get; set; }
        public CaseDetails CaseDetails { get; set; }

        public Data(Person advocate, Person respondent, CaseDetails caseDetails) {
            Advocate = advocate;
            Respondent = respondent;
            CaseDetails = caseDetails;
        }
    }
}
