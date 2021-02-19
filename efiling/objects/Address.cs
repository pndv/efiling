using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace efiling
{
    class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public int PinCode { get; set; }
    }
}
