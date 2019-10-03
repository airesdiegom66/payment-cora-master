using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Commands.Results
{
    public class GetCreditCardPaymentOrderResult : GetPaymentOrderResult
    {
        public int MinParcel { get; private set; }
        public int MaxParcel { get; private set; }

        public GetCreditCardPaymentOrderResult(int minParcel, int maxParcel,
            int paymentOrderId, decimal total, DateTime expiredDate)
            : base(
                  paymentOrderId,
                  total,
                  expiredDate)
        {
            MinParcel = minParcel;
            MaxParcel = maxParcel;
        }
    }
}
