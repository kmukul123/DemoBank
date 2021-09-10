using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TransactionService
{
    public class TransactionVM : ITransaction
    {
        public TransactionVM()
        {

        }
        public TransactionVM(ITransaction transaction)
        {
            this.ExternalId = transaction.ExternalId;
            this.FromAccount = transaction.FromAccount;
            this.ToAccount = transaction.ToAccount;
            this.Description = transaction.Description;
            this.Amount = transaction.Amount;
            this.Date = transaction.Date;

            if (transaction.Owner != null)
                this.Owner = new CustomerVM(transaction.Owner);

        }

        [JsonPropertyName("id")]
        public Guid ExternalId { get; set; }
        [JsonPropertyName("fromAccount")]
        public string FromAccount { get ; set ; }
        [JsonPropertyName("toAccount")]
        public string ToAccount { get ; set ; }

        [JsonPropertyName("description")]
        public string Description { get ; set ; }
        [JsonPropertyName("amount")]
        public decimal Amount { get ; set ; }
        [JsonPropertyName("date")]
        public DateTime Date { get ; set ; }
        
        [JsonIgnore]
        public Guid OwnerId { get => Owner.Id; }
        [JsonPropertyName("owner")]
        public CustomerVM Owner { get ; set ; }
        [JsonIgnore]
        ICustomer ITransaction.Owner { get => Owner; }
    }

    public class CustomerVM : ICustomer
    {
        public CustomerVM()
        {

        }
        public CustomerVM(ICustomer c)
        {
            this.Id = c.Id;
            this.Name = c.Name;
        }
        [JsonPropertyName("id")]
        public Guid Id { get ; set ; }
        [JsonPropertyName("name")]
        public string Name { get ; set ; }
    }
}
