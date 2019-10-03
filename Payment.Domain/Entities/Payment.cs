using Flunt.Validations;
using Payment.Domain.ValueObjects;
using Payment.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Entities
{
    public abstract class Payment : Entity
    {
        protected Payment(DateTime paidDate, DateTime expiredDate, decimal amount, decimal amountPaid, Name payer, Document document, Email email)
        {
            Number = Guid.NewGuid().ToString();
            PaidDate = paidDate;
            ExpiredDate = expiredDate;
            Amount = amount;
            AmountPaid = amountPaid;
            Payer = payer;
            Document = document;            
            Email = email;

            AddNotifications(new Contract()
                .Requires()                
                .IsGreaterOrEqualsThan(AmountPaid, 1, "Payment.AmountPaid", "O valor pago não pode menor que zero")
                .IsGreaterOrEqualsThan(PaidDate, DateTime.Today, "Payment.Payments", "A data do pagamento está vencida.")
            );
        }

        public string Number { get; private set; }
        public DateTime PaidDate { get; private set; }
        public DateTime ExpiredDate { get; private set; }
        public Decimal Amount { get; private set; }
        public Decimal AmountPaid { get; private set; }
        public Name Payer { get; private set; } // quem de fato vai pagar
        public Document Document { get; private set; } // doc de quem de fato vai pagar        
        public Email Email { get; private set; }
    }
}
