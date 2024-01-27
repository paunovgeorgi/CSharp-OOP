using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        private const int MLInterestRate = 3;
        private const double MLAmount = 50_000;
        public MortgageLoan() : base(MLInterestRate, MLAmount)
        {
        }
    }
}
