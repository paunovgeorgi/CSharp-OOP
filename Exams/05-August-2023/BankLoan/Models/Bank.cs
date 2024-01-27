using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private readonly List<ILoan> loans;
        private readonly List<IClient> clients;
        private string name;
        protected Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();
        }


        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }
        public int Capacity { get; private set; }
        public IReadOnlyCollection<ILoan> Loans => loans.AsReadOnly();
        public IReadOnlyCollection<IClient> Clients => clients.AsReadOnly();
        public double SumRates()
        {
            return loans.Sum(l => l.InterestRate);
        }

        public void AddClient(IClient Client)
        {
            if (clients.Count >= Capacity)
            {
                throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
            }
            clients.Add(Client);
        }

        public void RemoveClient(IClient Client)
        {
            clients.Remove(Client);
        }

        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {Name}, Type: {GetType().Name}");
            if (clients.Any())
            {
                sb.AppendLine($"Clients: {string.Join(", ", clients.Select(c=>c.Name))}");
            }
            else
            {
                sb.AppendLine("Clients: none");
            }
            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {loans.Sum(l => l.InterestRate)}");

            return sb.ToString().TrimEnd();
        }
    }
}
