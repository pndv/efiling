using System;
using System.Collections.Generic;
using System.Text;

namespace efiling
{
    class Person
    {
        public Person(string lastName, string firstName, string title, Address address) {
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            Address = address;
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public Address Address { get; set; }
    }
}
