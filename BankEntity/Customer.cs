using System;
using System.Collections.Generic;

#nullable disable

namespace BankRepository
{
    /// <summary>
    /// TODO: could have base entity classes
    /// </summary>
    public partial class Customer : DomainModel.ICustomer
    {
        public Customer()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        //not needed
        //public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
