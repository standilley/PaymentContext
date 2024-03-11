using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;
public abstract class Payment : Entity
{
    protected Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Document document, Address address, Email email)
    {
        Number = Guid.NewGuid().ToString().Replace("-","").Substring(0,10).ToUpper();
        PaidDate = paidDate;
        ExpireDate = expireDate;
        Total = total;
        TotalPaid = totalPaid;
        Payer = payer;
        Document = document;
        Address = address;
        Email = email;

        AddNotifications(new Contract<Payment>()
            .Requires()
            .IsLowerOrEqualsThan(0, Total, "Payment.Total", "O Total n�o pode ser zero")
            .IsGreaterOrEqualsThan(Total, TotalPaid, "Payment.TotalPaid", "O valor pago � menor que o valor do pagamento")
            );
    }

    public string Number { get; private set; } // gerar um numero/guid para identificar o pagamento
    public DateTime PaidDate { get; private set; }
    public DateTime ExpireDate { get; private set; }
    public decimal Total { get; private set; }
    public decimal TotalPaid { get; private set; }
    public string Payer { get; private set; } // pagador
    public Document Document { get; private set; }// documento da pessoa
    public Address Address { get; private set; } // aqui � o endere�o de cobran�a, que pode ser diferente
    public Email Email { get; private set; }     
}
