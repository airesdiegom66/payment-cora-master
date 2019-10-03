using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Commands.Results
{
    public class GetBoletoPaymentOrderResult : GetPaymentOrderResult
    {
        public int BoletoNumber { get; private set; }
        public string BarCode { get; private set; }
        
        public GetBoletoPaymentOrderResult(int boletoNumber, string barCode,
            int paymentOrderId, decimal total, DateTime expiredDate)
            : base(
                  paymentOrderId,
                  total,
                  expiredDate)
        {
            BoletoNumber = boletoNumber;
            BarCode = barCode;            
        }
    }
}
