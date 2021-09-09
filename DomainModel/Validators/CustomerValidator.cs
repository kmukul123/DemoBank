using System;
using System.Collections.Generic;
using System.Text;
using static DomainModel.Validators.ValidationsLibrary;

namespace DomainModel.Validators
{
    public class CustomerValidator : IValidator<ICustomer>
    {
        public bool Validate(ICustomer input)
        {
            HasMinLength(input.Name, 5, nameof(input.Name));

            return true;
        }
    }
}
