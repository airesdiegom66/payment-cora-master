using Flunt.Notifications;
using Flunt.Validations;

namespace Payment.Domain.ValueObjects
{
    public class Address : Notifiable
    {
        public Address(string street, string number, string district, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            District = district;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract()
                .Requires()                
                .IsNotNullOrEmpty(ZipCode, "Address.ZipCode", "O cep é obrigatório")
            );
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}
