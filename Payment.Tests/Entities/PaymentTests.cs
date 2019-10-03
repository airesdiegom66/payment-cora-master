using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment.Domain.Entities;
using Payment.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Tests.Entities
{
    [TestClass]
    public class PaymentTests
    {
        private readonly Name _name;
        private readonly Email _email;
        private readonly Document _document;
        private readonly Address _address;
        
        public PaymentTests()
        {
            _name = new Name("Diego", "Aires");
            _email = new Email("diego.aires@email.com");
            _document = new Document("337.020-09");            
        }

        [TestMethod]
        public void Should_Return_Error_When_Boleto_AmountPaid_Is_Invalid()
        {
            var boletoPayment = new BoletoPayment(123, "4444.5555.6666.9999", DateTime.Today,
                DateTime.Today.AddDays(7), 10, 9, _name, _document, _email);

            //var msgValidation = "";
            //foreach (var nf in boletoPayment.Notifications)
            //{
            //    msgValidation = nf.Message;
            //}
            Assert.IsTrue(boletoPayment.Invalid);
        }

        [TestMethod]
        public void Should_Return_Success_When_Boleto_AmountPaid_Is_Valid()
        {
            var boletoPayment = new BoletoPayment(123, "4444.5555.6666.9999", DateTime.Today,
                DateTime.Today.AddDays(7), 10, 10, _name, _document, _email);

            Assert.IsTrue(boletoPayment.Valid);
        }

        [TestMethod]
        public void Should_Return_Error_When_CreditCard_AmountPaid_Is_Invalid()
        {
            var creditCardPayment = new CreditCardPayment("123456", "3333.4444.5555.66", DateTime.Parse("2022/02/01"), 222,
                DateTime.Today, DateTime.Today.AddDays(7), 10, 0, _name, _document, _email);

            //var msgValidation = "";
            //foreach (var nf in creditCardPayment.Notifications)
            //{
            //    msgValidation = nf.Message;
            //}
            Assert.IsTrue(creditCardPayment.Invalid);
        }

        [TestMethod]
        public void Should_Return_Success_When_CreditCard_AmountPaid_Is_Valid()
        {
            var creditCardPayment = new CreditCardPayment("123456", "5117.5484.0010.9227", DateTime.Parse("2022/02/21"), 222,
                DateTime.Today, DateTime.Today.AddDays(7), 10, 1, _name, _document, _email);

            Assert.IsTrue(creditCardPayment.Valid);
        }
    }
}
