using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable<Notification>,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreateCreditCardSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
        
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailservice)
        {
            _repository = repository;
            _emailService = emailservice;
        }
        public ICommandResult handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Validation
            command.Validate();
            if (command.IsValid)
                AddNotifications(command);
            else
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // Verificar se o Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");
            // Verificar se E-mail já está cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var address = new Address(
                command.Street, 
                command.Number,
                command.Neighborhood,
               command.City ,
               command.State,
               command.Country,
               command.ZipCode);
            var email = new Email(command.Email);

            // Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            // só muda a implementação do Payment
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
                );
            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar Validadações 
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.SendEmail(
                student.Name.ToString(), 
                student.Email.Address,
                "bem vindo",
                "Sua assinatura foi criada");
            
            // Retornar informações
            return new CommandResult(true, "Assinatura Realizada com sucesso");
        }

        public ICommandResult handle(CreateCreditCardSubscriptionCommand command)
        {
            // Fail Validation
            command.Validate();
            if (command.IsValid)
                AddNotifications(command);
            else
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // Verificar se o Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");
            // Verificar se E-mail já está cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
               command.City,
               command.State,
               command.Country,
               command.ZipCode);
            var email = new Email(command.Email);

            // Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            // só muda a implementação do Payment
            var payment = new CreditCardPayment(
                command.CardHolderName,
                command.CardNumber,
                command.LastTransactionNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
                );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar Validadações 
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.SendEmail(
                student.Name.ToString(),
                student.Email.Address,
                "bem vindo",
                "Sua assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura Realizada com sucesso");
        }

        public ICommandResult handle(CreatePayPalSubscriptionCommand command)
        {
            // Fail Validation
            command.Validate();
            if (command.IsValid)
                AddNotifications(command);
            else
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // Verificar se o Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");
            // Verificar se E-mail já está cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
               command.City,
               command.State,
               command.Country,
               command.ZipCode);
            var email = new Email(command.Email);

            // Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            // só muda a implementação do Payment
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
                );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar Validadações 
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações
            if (IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.SendEmail(
                student.Name.ToString(),
                student.Email.Address,
                "bem vindo",
                "Sua assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura Realizada com sucesso");
        }
    }
}
