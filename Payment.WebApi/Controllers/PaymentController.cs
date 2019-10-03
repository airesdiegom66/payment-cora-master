using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Domain.Commands.Handlers;
using Payment.Domain.Commands.Inputs;
using Payment.Domain.Commands.Results;
using Payment.Domain.Enums;
using Payment.Domain.Repositories;

namespace Payment.WebApi.Controllers
{

    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentCommandHandler _handler;
        private readonly IPaymentOrderRepository _paymentOrderRepository;

        private static List<GetBoletoPaymentOrderResult> _listBoletoOrders = new List<GetBoletoPaymentOrderResult>();
        private static List<GetCreditCardPaymentOrderResult> _listCreditCardOrders = new List<GetCreditCardPaymentOrderResult>();
        
        public PaymentController(PaymentCommandHandler handler, IPaymentOrderRepository paymentOrderRepository)
        {
            _handler = handler;
            _paymentOrderRepository = paymentOrderRepository;
        }

        [HttpGet]
        [Route("api/v1/order/boleto")]

        public IActionResult GetBoleto()
        {
            var paymentOrder = _paymentOrderRepository.GenerateBoletoPaymentOrder();
            _listBoletoOrders.Add(paymentOrder);
            return Ok(paymentOrder);
        }

        [HttpGet]
        [Route("api/v1/order/creditcard")]

        public IActionResult GetCreditCard()
        {
            var paymentOrder = _paymentOrderRepository.GenerateCreditCardPaymentOrder();
            _listCreditCardOrders.Add(paymentOrder);
            return Ok(paymentOrder);
        }

        [HttpPost]
        [Route("api/v1/payment/boleto")]
        public async Task<IActionResult> PostBoleto([FromBody] RegisterBoletoPaymentCommand command)
        {
            try
            {
                //Este trecho de código é um ajuste por causa da ordem de pagamento que é gerada como mock
                var boleto = _listBoletoOrders.FirstOrDefault(a => a.BarCode.Equals(command.BarCode));
                if(boleto == null)
                    return BadRequest(new { success = false, errors = new[] { "O boleto não existe, verifique o código de barras." } });

                command.BoletoNumber = boleto.BoletoNumber;
                command.ExpireDate   = boleto.ExpiredDate;
                command.Total        = boleto.Total;

                var result = _handler.Handle(command);

                if (!_handler.Notifications.Any())
                    return Ok(new { success = true, data = result });
                else
                    return BadRequest(new { success = false, errors = _handler.Notifications });
            }
            catch (Exception)
            {
                //Logar e tratar exceção
                throw;
            }
        }

        [HttpPost]
        [Route("api/v1/payment/creditcard")]
        public async Task<IActionResult> PostCreditCard([FromBody] RegisterCreditCardPaymentCommand command)
        {
            try
            {
                //Este trecho de código é um ajuste por causa da ordem de pagamento que é gerada como mock
                var boleto = _listCreditCardOrders.FirstOrDefault(a => a.PaymentOrderId.ToString().Equals(command.PaymentNumber));
                if (boleto == null)
                    return BadRequest(new { success = false, errors = new[] { "A ordem de pagamento não existe, gere a ordem de pagamento." } });

                command.ExpireDate = boleto.ExpiredDate;
                command.Total = boleto.Total;

                var result = _handler.Handle(command);

                if (!_handler.Notifications.Any())
                    return Ok(new { success = true, data = result });
                else
                    return BadRequest(new { success = false, errors = _handler.Notifications });
            }
            catch (Exception)
            {
                //Logar e tratar exceção
                throw;
            }
        }
    }
}