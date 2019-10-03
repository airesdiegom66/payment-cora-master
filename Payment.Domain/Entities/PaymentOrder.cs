using Payment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Entities
{
    public class PaymentOrder
    {
        public PaymentOrder(decimal total, DateTime expiredDate, string barCode, int maxParcel, EPaymentType paymentType)
        {
            Random random = new Random();
            
            Id = random.Next(0, 100);
            Total = total;
            ExpiredDate = expiredDate;
            BarCode = barCode;
            MaxParcel = maxParcel;
            PaymentType = paymentType;
        }

        public int Id { get; private set; }
        public decimal Total { get; private set; }
        public DateTime ExpiredDate { get; private set; }
        public string BarCode { get; private set; }
        public int MaxParcel { get; private set; }
        public EPaymentType PaymentType { get; private set; }
    }
}
