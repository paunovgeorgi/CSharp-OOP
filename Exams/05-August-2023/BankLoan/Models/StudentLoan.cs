using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class StudentLoan : Loan
    {
        private const int SLInterestRate = 1;
        private const double SLAmount = 10_000;
        public StudentLoan() : base(SLInterestRate, SLAmount)
        {
        }
    }
}
