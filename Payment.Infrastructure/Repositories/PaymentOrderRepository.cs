using Payment.Domain.Commands.Results;
using Payment.Domain.Entities;
using Payment.Domain.Enums;
using Payment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Infrastructure.Repositories
{
    public class PaymentOrderRepository : IPaymentOrderRepository
    {
        public GetBoletoPaymentOrderResult GenerateBoletoPaymentOrder()
        {
            Random random = new Random();
            int orderId = random.Next(1, 100);
            int boletoNumber = random.Next(100, 999);
            decimal total = random.Next(100, 10000);
            string barCode = random.Next(10000, 1000000).ToString();

            return new GetBoletoPaymentOrderResult(boletoNumber, barCode, orderId, total, DateTime.Today.AddDays(4));
        }
        public GetCreditCardPaymentOrderResult GenerateCreditCardPaymentOrder()
        {
            Random random = new Random();
            int orderId = random.Next(1, 100);
            int boletoNumber = random.Next(100, 999);
            decimal total = random.Next(100, 10000);
            string barCode = random.Next(10000, 1000000).ToString();

            return new GetCreditCardPaymentOrderResult(1, 5, orderId, total, DateTime.Today.AddDays(4));
        }
    }
}
