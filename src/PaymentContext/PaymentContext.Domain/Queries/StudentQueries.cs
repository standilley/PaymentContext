using PaymentContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Queries
{
    public static class StudentQueries
    {
        public static Expression<Func<Student, bool>> GetSudentInfo(string document)
        {
            return x => x.Document.Number == document;
        }
    }
}
