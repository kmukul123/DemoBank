using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public interface ICustomer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
