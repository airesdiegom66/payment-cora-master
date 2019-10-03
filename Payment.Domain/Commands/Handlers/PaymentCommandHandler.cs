using Flunt.Notifications;
using Payment.Domain.Commands.Inputs;
using Payment.Domain.Entities;
using Payment.Domain.Repositories;
using Payment.Domain.Services;
using Payment.Domain.ValueObjects;
using Payment.Shared.Commands;
using Payment.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Commands.Handlers
{
    public class PaymentCommandHandler : Notifiable,
        ICommandHandler<RegisterBoletoPaymentCommand>,
        ICommandHandler<RegisterCreditCardPaymentCommand>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly ICreditCardService _creditCardService;

        public PaymentCommandHandler(IBuyerRepository buyerRepository, ICreditCardService creditCardService)
        {
            _buyerRepository   = buyerRepository;
            _creditCardService = creditCardService;
        }

        public ICommandResult Handle(RegisterBoletoPaymentCommand command)
        {
            //Passo 1. Criar um objeto Comprador
            var name = new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);
            var document = new Document(command.Document);
            var buyer = new Buyer(name, email, document);

            //Passo 2. Valida o objeto antes de ir à base: Fail Fast Validation
            AddNotifications(buyer.Name.Notifications);
            AddNotifications(buyer.Document.Notifications);
            AddNotifications(buyer.Email.Notifications);

            if (Invalid)
                return new CommandResult(false, "Dados do comprador está inválido.");
            
            //Passo 3. Checar se o boleto é válido
            var boletoPayment = new BoletoPayment(command.BoletoNumber, command.BarCode, DateTime.Today,
                command.ExpireDate, command.Total, command.TotalPaid, name, document, email);

            AddNotifications(boletoPayment.Notifications);

            if (Invalid)
                return new CommandResult(false, "Dados do boleto está inválido.");

            //Passo 4. Boleto e usuário válidos: agora associa o pagamento ao usuário (comprador)
            buyer.AddPayment(boletoPayment);

            AddNotifications(buyer.Notifications);

            if (Invalid)
                return new CommandResult(false, "Não foi possível registrar o pagamento do boleto.");

            //Passo 5. Salva o pagamento na base de dados
            _buyerRepository.Save(buyer);

            // Retornar informações
            return new CommandResult(true, "Pagamento do boleto efetuado com sucesso!");
        }

        public ICommandResult Handle(RegisterCreditCardPaymentCommand command)
        {
            //Passo 1. Criar um objeto Comprador
            var name = new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);
            var document = new Document(command.Document);
            var buyer = new Buyer(name, email, document);

            //Passo 2. Valida o objeto antes de ir à base: Fail Fast Validation
            AddNotifications(buyer.Name.Notifications);
            AddNotifications(buyer.Document.Notifications);
            AddNotifications(buyer.Email.Notifications);

            if (Invalid)
                return new CommandResult(false, "Dados do comprador está inválido.");
                        
            //Passo 3. Checar se os dados do cartão é válido
            var creditCardPayment = new CreditCardPayment(command.CardHolderName, command.CardNumber, command.CardExpirationDate, command.CVV,
                DateTime.Today, command.ExpireDate, command.Total, command.TotalPaid, name, document, email);

            AddNotifications(creditCardPayment.Notifications);

            if (Invalid)
                return new CommandResult(false, "Dados do cartão do crédito estão incorretos.");

            //Passo 4. Consultar serviço de terceiro para validar o cartão de crédito
            if (!_creditCardService.CheckOwnerCreditCard(command.Document, command.CardHolderName, command.CardNumber, command.CardExpirationDate, command.CVV))
                return new CommandResult(false, "Cartão inválido.");

            //Passo 5. Salva o pagamento na base de dados
            _buyerRepository.Save(buyer);

            // Retornar informações
            return new CommandResult(true, "Pagamento com o cartão de credito feito com sucesso!");

        }
    }
}
