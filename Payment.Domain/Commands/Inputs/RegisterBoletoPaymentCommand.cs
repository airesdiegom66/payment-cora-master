using Payment.Shared.Commands;
using System;

namespace Payment.Domain.Commands.Inputs
{
    public class RegisterBoletoPaymentCommand : ICommand
    {
        //Buyer
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        
        //Boleto        
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }        
        public string PayerEmail { get; set; }

        public int BoletoNumber { get; set; }
        public string BarCode { get; set; }
        
        //Address
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
