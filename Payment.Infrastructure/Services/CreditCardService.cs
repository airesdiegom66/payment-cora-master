using Payment.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Infrastructure.Services
{
    public class CreditCardService : ICreditCardService
    {
        public bool CheckOwnerCreditCard(string cpf, string cardHolderName, string cardNo, DateTime expiryDate, int cvv)
        {
            //consumiria um serviço de terceiro para checar se o cartão é de fato do pagamente
            return true;
        }
    }
}
