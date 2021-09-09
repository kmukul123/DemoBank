using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public interface ITransaction
    {
        public int RowId { get; set; }
        public Guid ExternalId { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid OwnerId { get; set; }

        public ICustomer Owner { get; }
    }
}
