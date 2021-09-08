using System;
using System.Collections.Generic;

#nullable disable

namespace BankRepository
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public Guid? Owner { get; set; }

        public virtual Customer OwnerNavigation { get; set; }
    }
}
