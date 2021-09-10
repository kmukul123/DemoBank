using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TransactionService
{
    public class TransactionVM : ITransaction
    {
        public TransactionVM(ITransaction transaction)
        {
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

        [JsonPropertyName("Id")]
        public Guid ExternalId { get; set; }
        public string FromAccount { get ; set ; }
        public string ToAccount { get ; set ; }
        public string Description { get ; set ; }
        public decimal Amount { get ; set ; }
        public DateTime Date { get ; set ; }
        public Guid OwnerId { get ; set ; }
        public ICustomer Owner { get ; set ; }
    }
}
