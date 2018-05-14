using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternConsole.Prototype
{
    public class Address2
    {
        public string StreetAddress, City, Country;

        public Address2(string streetAddress, string city, string country)
        {
            StreetAddress = streetAddress ?? throw new ArgumentNullException(paramName: nameof(streetAddress));
            City = city ?? throw new ArgumentNullException(paramName: nameof(city));
            Country = country ?? throw new ArgumentNullException(paramName: nameof(country));
        }

        public Address2(Address2 other)
        {
            StreetAddress = other.StreetAddress;
            City = other.City;
            Country = other.Country;
        }

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Country)}: {Country}";
        }
    }

    public class Employee2
    {
        public string Name;
        public Address2 Address;

        public Employee2(string name, Address2 address)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        public Employee2(Employee2 other)
        {
            Name = other.Name;
            Address = new Address2(other.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }
}
