using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment.Domain.Entities;
using Payment.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Tests.Entities
{
    [TestClass]
    public class BuyerTests
    {
        private readonly Name _name;
        private readonly Email _email;
        private readonly Document _document;
        private readonly Email _emailInvalid;
        private readonly Document _documentInvalid;

        private Buyer _buyer;
        
        public BuyerTests()
        {
            _name     = new Name("Diego", "Aires");
            _email    = new Email("diego.aires@email.com");
            _document = new Document("337.522.020-09");

            _emailInvalid    = new Email("diego.airemail.com");
            _documentInvalid = new Document("337.020-09");            
        }

        [TestMethod]
        public void Should_Return_Error_When_Boleto_Buyer_Has_Invalid_Data()
        {
            var boletoPayment = new BoletoPayment(123, "4444.5555.6666.9999", DateTime.Today,
                DateTime.Today.AddDays(7), 10, 10, _name, _document, _email);

            _buyer = new Buyer(_name, _emailInvalid, _documentInvalid);
            _buyer.AddPayment(boletoPayment);

            //var msgValidation = "";
            //foreach (var nf in _buyer.Notifications)
            //{
            //    msgValidation = nf.Message;
            //}

            Assert.IsTrue(_buyer.Invalid);
        }

        [TestMethod]
        public void Should_Return_Success_When_Boleto_Buyer_Has_Valid_Data()
        {
            var boletoPayment = new BoletoPayment(123, "4444.5555.6666.9999", DateTime.Today,
                DateTime.Today.AddDays(7), 10, 10, _name, _document, _email);

            _buyer = new Buyer(_name, _email, _document);
            _buyer.AddPayment(boletoPayment);
            
            Assert.IsTrue(_buyer.Valid);
        }

        [TestMethod]
        public void Should_Return_Error_When_CreditCard_Buyer_Has_Invalid_Data()
        {
            var creditCardPayment = new CreditCardPayment("123456", "3333.4444.5555.6666", DateTime.Parse("2022/02/01"), 222,
                DateTime.Today, DateTime.Today.AddDays(7), 10, 0, _name, _document, _email);

            _buyer = new Buyer(_name, _email, _document);
            _buyer.AddPayment(creditCardPayment);

            Assert.IsTrue(_buyer.Valid);
        }

        [TestMethod]
        public void Should_Return_Success_When_CreditCard_Buyer_Has_Valid_Data()
        {
            var creditCardPayment = new CreditCardPayment("123456", "3333.4444.5555.6666", DateTime.Parse("2022/02/01"), 222,
                DateTime.Today, DateTime.Today.AddDays(7), 10, 0, _name, _document,  _email);

            _buyer = new Buyer(_name, _email, _document);
            _buyer.AddPayment(creditCardPayment);

            Assert.IsTrue(_buyer.Valid);
        }

        [TestMethod]
        public void Should_Return_Error_When_Boleto_Overdue_Payment()
        {
            var boletoPayment = new BoletoPayment(123, "4444.5555.6666.9999", DateTime.Today.AddDays(-1),
                DateTime.Today.AddDays(-7), 10, 10, _name, _document, _email);

            _buyer = new Buyer(_name, _email, _document);
            _buyer.AddPayment(boletoPayment);

            //var msgValidation = "";
            //foreach (var nf in _buyer.Notifications)
            //{
            //    msgValidation = nf.Message;
            //}

            Assert.IsTrue(_buyer.Invalid);
        }

        [TestMethod]
        public void Should_Return_Error_When_CreditCard_Overdue_Payment()
        {
            var creditCardPayment = new CreditCardPayment("123456", "3333.4444.5555.6666", DateTime.Parse("2022/02/01"), 222,
                DateTime.Today.AddDays(-1), DateTime.Today.AddDays(-7), 10, 0, _name, _document, _email);

            _buyer = new Buyer(_name, _email, _document);
            _buyer.AddPayment(creditCardPayment);

            Assert.IsTrue(_buyer.Invalid);
        }
    }
}
