using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Services
{
    public interface ICreditCardService
    {
        bool CheckOwnerCreditCard(string cpf, string cardHolderName, string cardNo, DateTime expiryDate, int cvv);
    }
}
