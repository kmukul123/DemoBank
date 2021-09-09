using DomainModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace Repository
{
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

            if (transaction.OwnerNavigation != null)
                this.OwnerId = transaction.OwnerNavigation.Id;
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
        ICustomer ITransaction.OwnerNavigation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
