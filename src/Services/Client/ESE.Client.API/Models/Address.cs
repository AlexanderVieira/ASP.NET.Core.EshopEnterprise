using ESE.Core.DomainObjects;
using System;

namespace ESE.Client.API.Models
{
    public class Address : Entity
    {
        public string PublicPlace { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string District { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid CustomerId { get; private set; }
        
        // EF Relation
        public Customer Customer { get; protected set; }

        public Address(string publicPlace, string number, string complement, string district, string zipCode, string city, string state)
        {
            PublicPlace = publicPlace;
            Number = number;
            Complement = complement;
            District = district;
            ZipCode = zipCode;
            City = city;
            State = state;
        }
    }
}