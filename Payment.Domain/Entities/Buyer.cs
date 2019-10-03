using Flunt.Validations;
using Payment.Domain.ValueObjects;
using Payment.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payment.Domain.Entities
{
    public class Buyer : Entity
    {
        private IList<Payment> _payments;

        public Buyer(Name name, Email email, Document document)
        {
            Name = name;
            Email = email;
            Document = document;
            _payments = new List<Payment>();

            //agrupando os erros usando o fluent validator
            AddNotifications(name, email, document);
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Document Document { get; private set; }
        public Address DeliveryAddress { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterOrEqualsThan(payment.PaidDate, DateTime.Today,  "Buyer.Payments", "A data do pagamento está vencida.")
            );

            if (Valid)
                _payments.Add(payment);
        }
    }
}


