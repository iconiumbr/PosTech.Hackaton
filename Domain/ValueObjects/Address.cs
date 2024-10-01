using Domain.Shared;

namespace Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string complement, string zipCode,
            string neighborhood, string city, string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            ZipCode = zipCode;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string ZipCode { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ZipCode;
            yield return Number;
        }
    }
}
