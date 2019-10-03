using Flunt.Validations;
using Payment.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public BoletoPayment(int boletoNumber, string barCode,
            DateTime paidDate, DateTime expiredDate, decimal amount, decimal amountPaid, Name payer, Document document, Email email)
            : base(
                  paidDate,
                  expiredDate,
                  amount,
                  amountPaid,
                  payer,
                  document,                  
                  email)
        {
            BoletoNumber = boletoNumber;
            BarCode = barCode;

            //cartão de crédito aceita pagamento mínimo
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterOrEqualsThan(AmountPaid, Amount, "Payment.AmountPaid", "O valor pago está menor que o valor do pagamento")
            );
        }

        public int BoletoNumber { get; private set; }
        public string BarCode { get; private set; }
    }
}
