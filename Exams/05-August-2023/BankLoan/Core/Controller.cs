using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;

namespace BankLoan.Core
{
    public class Controller : IController
    {

        private LoanRepository loans = new LoanRepository();
        private BankRepository banks = new BankRepository();
        public string AddBank(string bankTypeName, string name)
        {
            if (bankTypeName != nameof(BranchBank) && bankTypeName != nameof(CentralBank))
            {
                throw new ArgumentException(ExceptionMessages.BankTypeInvalid);
            }

            IBank bank = null;
            if (bankTypeName == nameof(BranchBank))
            {
                bank = new BranchBank(name);
            }
            else
            {
                bank = new CentralBank(name);
            }
            banks.AddModel(bank);
            return String.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }

        public string AddLoan(string loanTypeName)
        {
            if (loanTypeName != nameof(StudentLoan) && loanTypeName != nameof(MortgageLoan))
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
            }

            ILoan loan = null;
            if (loanTypeName == nameof(StudentLoan))
            {
                loan = new StudentLoan();
            }
            else
            {
                loan = new MortgageLoan();
            }
            loans.AddModel(loan);
            return String.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            IBank currentBank = banks.Models.FirstOrDefault(b=>b.Name == bankName);
            if (!loans.Models.Any(m=>m.GetType().Name == loanTypeName))
            {
                //throw new ArgumentException(ExceptionMessages.MissingLoanFromType, loanTypeName);
                throw new ArgumentException($"Loan of type {loanTypeName} is missing.");
            }
            ILoan loan = loans.Models.FirstOrDefault(l => l.GetType().Name == loanTypeName);
            currentBank.AddLoan(loan);
            loans.RemoveModel(loan);
            return String.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {

            if (clientTypeName != nameof(Student) && clientTypeName != nameof(Adult))
            {
                throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
            }
            IBank currentBank = banks.Models.FirstOrDefault(b => b.Name == bankName);

            IClient client = null;
            if (clientTypeName == nameof(Student))
            {
                client = new Student(clientName,id,income);
                if (currentBank.GetType().Name == nameof(CentralBank))
                {
                    return String.Format(OutputMessages.UnsuitableBank);
                }
            }
            else
            {
                client = new Adult(clientName, id, income);
                if (currentBank.GetType().Name == nameof(BranchBank))
                {
                    return String.Format(OutputMessages.UnsuitableBank);
                }
            }
            currentBank.AddClient(client);
            return String.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
        }

        public string FinalCalculation(string bankName)
        {
            IBank currentBank = banks.Models.FirstOrDefault(b => b.Name == bankName);

            double incomeSum = currentBank.Clients.Sum(c => c.Income);
            double loanAmountSum = currentBank.Loans.Sum(l => l.Amount);
            double totalSum = incomeSum + loanAmountSum;

            return String.Format(OutputMessages.BankFundsCalculated,bankName, $"{totalSum:f2}");
        }

        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IBank bank in banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
