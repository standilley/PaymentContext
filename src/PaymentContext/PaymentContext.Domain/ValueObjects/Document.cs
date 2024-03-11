using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract<Document>()
                .Requires()
                .IsTrue(Validade(), "Document.Number", "Documento Inválido")
                );
        }

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }
        private bool Validade()
        {
            if (Type == EDocumentType.CNPJ && Number.Length == 14)
                return true;
            if(Type == EDocumentType.CPF && Number.Length == 11)
                return true;
            return false;
        }
    }
}

