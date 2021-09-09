using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Repository
{
    /// <summary>
    /// todo could have base entity class
    /// </summary>
    public partial class Transaction : ITransaction
    {
        public Transaction()
        {
        }

        /// <summary>
        /// TODO we could use automapper for it
        /// </summary>
        /// <param name="transaction"></param>
        public Transaction(ITransaction transaction)
        {
            this.RowId = transaction.RowId;
            this.ExternalId = transaction.ExternalId;
            this.FromAccount = transaction.FromAccount;
            this.ToAccount = transaction.ToAccount;
            this.Description = transaction.Description;
            this.Amount = transaction.Amount;
            this.Date = transaction.Date;
            this.OwnerId = transaction.OwnerId;

            if (transaction.Owner != null)
                this.OwnerId = transaction.Owner.Id;
        }

        public int RowId { get; set; }
        public Guid ExternalId { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid OwnerId { get; set; }

        public virtual Customer OwnerNavigation { get; set; }
        [NotMapped]
        public ICustomer Owner 
        { 
            get => OwnerNavigation; 
            set { OwnerNavigation = new Customer(value);
                OwnerId = value.Id;
            } 
        }
    }
}
