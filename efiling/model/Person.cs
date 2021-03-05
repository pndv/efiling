namespace efiling.model {
    public class Person {
        public Person(string lastName,
                      string firstName,
                      string orgTitle,
                      Address address) {
            LastName = lastName;
            FirstName = firstName;
            OrgTitle = orgTitle;
            Address = address;
        }

        public string LastName { get; }
        public string FirstName { get; }
        public string OrgTitle { get; }
        public Address Address { get; }
    }
}