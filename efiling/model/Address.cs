namespace efiling.model {
    public class Address {
        public Address(string line1,
                       string? line2,
                       string? line3,
                       string city,
                       string? district,
                       string state,
                       int pinCode) {
            Line1 = line1;
            Line2 = line2;
            Line3 = line3;
            City = city;
            District = district;
            State = state;
            PinCode = pinCode;
        }

        public string Line1 { get; }
        public string? Line2 { get; }
        public string? Line3 { get; }
        public string City { get; }
        public string? District { get; }
        public string State { get; }
        public int PinCode { get; }

        public string SingleLineAddress =>
            Line1 + ", "
                  + (string.IsNullOrEmpty(Line2) ? "" : Line2 + ", ")
                  + (string.IsNullOrEmpty(Line3) ? "" : Line2 + ", ")
                  + City + ", "
                  + (string.IsNullOrEmpty(District) ? "" : Line2 + ", ")
                  + State + "--" + PinCode;
    }
}