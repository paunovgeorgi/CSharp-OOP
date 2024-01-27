using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private readonly List<ILoan> loans = new List<ILoan>();
        public IReadOnlyCollection<ILoan> Models => loans.AsReadOnly();
        public void AddModel(ILoan model)
        {
            loans.Add(model);
        }

        public bool RemoveModel(ILoan model)
        {
           return  loans.Remove(model);
        }

        public ILoan FirstModel(string name)
        {
            return loans.FirstOrDefault(l => l.GetType().Name == name);
        }
    }
}
