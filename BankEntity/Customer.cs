using DomainModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace Repository
{
    /// <summary>
    /// TODO: could have base entity classes
    /// </summary>
    public partial class Customer : ICustomer
    {
        public Customer()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Customer(ICustomer c)
        {
            this.Id = c.Id;
            this.Name = c.Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        //not needed
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
