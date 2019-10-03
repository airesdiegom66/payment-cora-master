using Payment.Domain.Enums;
using Payment.Shared.Commands;
using System;

namespace Payment.Domain.Commands.Results
{
    public class GetPaymentOrderResult : ICommandResult
    {
        public GetPaymentOrderResult(int paymentOrderId, decimal total, DateTime expiredDate)
        {
            PaymentOrderId = paymentOrderId;
            Total = total;
            ExpiredDate = expiredDate;
        }

        public int PaymentOrderId { get; private set; }
        public decimal Total { get; private set; }
        public DateTime ExpiredDate { get; private set; }
    }
}
