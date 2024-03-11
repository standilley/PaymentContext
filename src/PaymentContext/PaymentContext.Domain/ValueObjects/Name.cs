using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Name>()
                .Requires()
                .IsLowerOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome deve Conter pelo menos 3 caracteres")
                .IsLowerOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome deve Conter pelo menos 3 caracteres")
                .IsGreaterOrEqualsThan(FirstName, 40, "Name.FirstName", "Nome deve Conter até 40 caracteres")
                );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
