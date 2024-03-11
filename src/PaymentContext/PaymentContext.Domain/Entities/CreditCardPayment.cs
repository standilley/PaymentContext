using PaymentContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(
            string cardHolderName, 
            string cardNumber, 
            string lastTransactionNumber,
            DateTime paidDate,
            DateTime expireDate, 
            decimal total, 
            decimal totalPaid, 
            string payer, 
            Document document, 
            Address address, 
            Email email)
            :base(
                 paidDate,
                 expireDate,
                 total,
                 totalPaid, 
                 payer,
                 document,
                 address,
                 email
                 )
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; private set; } // nome do titular do cartão
        public string CardNumber { get; private set; } // apenas os 4 ultimos numéros
        public string LastTransactionNumber { get; private set; }// numero da ultima transação
    }
}
