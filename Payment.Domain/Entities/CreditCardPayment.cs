using Flunt.Validations;
using Payment.Domain.ValueObjects;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Payment.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public DateTime CardExpirationDate { get; private set; }
        public int CVV { get; private set; }

        public CreditCardPayment(string cardHolderName, string cardNumber, DateTime cardExpirationDate, int cVV,
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
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            CardExpirationDate = cardExpirationDate;
            CVV = cVV;

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(IsCreditCardInfoValid(cardNumber, cardExpirationDate, cVV.ToString()), "CreditCardPayment.Number", "Cartão de credito inválido")
            );
        }
        public static bool IsCreditCardInfoValid(string cardNo, DateTime expiryDate, string cvv)
        {
            var cvvCheck = new Regex(@"^\d{3}$");

            cardNo = cardNo.Replace("-", "").Replace(" ", "").Replace(".", "");
            long nCard = 1;

            if (!long.TryParse(cardNo, out nCard))
                return false;

            if (cardNo.Length <= 0 && cardNo.Length > 16)
                return false;

            if (!cvvCheck.IsMatch(cvv)) //check cvv is valid as "999"
                return false;

            if (expiryDate.Date < DateTime.Today.Date)
                return false;

            return true;
        }
    }
}
