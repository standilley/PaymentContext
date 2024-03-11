using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Commands
{
    public class CreatePayPalSubscriptionCommand : Notifiable<Notification>, ICommand
    {   //student.cs
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        
        //paypalpayment.cs
        public string TransactionCode { get; set; }
        public string PaymentNumber { get; set; } 
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; } 
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }

        //Address.cs
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PayerEmail { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<string>()
                .Requires()
                .IsLowerOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome deve Conter pelo menos 3 caracteres")
                .IsLowerOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome deve Conter pelo menos 3 caracteres")
                .IsGreaterOrEqualsThan(FirstName, 40, "Name.FirstName", "Nome deve Conter até 40 caracteres")
                );
        }
    }
}
