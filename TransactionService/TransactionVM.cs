using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService
{
    public class TransactionVM : ITransaction
    {
        public int RowId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid ExternalId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FromAccount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ToAccount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid OwnerId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICustomer Owner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
