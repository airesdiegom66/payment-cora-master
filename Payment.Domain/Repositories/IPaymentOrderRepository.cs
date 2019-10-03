using Payment.Domain.Commands.Results;
using Payment.Domain.Entities;
using Payment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Repositories
{
    public interface IPaymentOrderRepository
    {
        GetBoletoPaymentOrderResult GenerateBoletoPaymentOrder();
        GetCreditCardPaymentOrderResult GenerateCreditCardPaymentOrder();
    }
}
