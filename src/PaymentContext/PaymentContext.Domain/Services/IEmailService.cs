using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string email, string subject, string body);
    }
}
