using System;
using System.Collections.Generic;
using System.Text;
using static DomainModel.Validators.ValidationsLibrary;

namespace DomainModel.Validators
{
    /// <summary>
    /// TODO: can add more validations
    /// could use a Gaurd classes for more readable code
    /// can add more logging
    /// </summary>
    public class TransactionValidator : IValidator<ITransaction>
    {
        private readonly IValidator<ICustomer> customerValidator;

        public TransactionValidator(IValidator<ICustomer> customerValidator)
        {
            this.customerValidator = customerValidator;
        }
        public bool Validate(ITransaction input)
        {
            HasMinLength(input.Description, 5, nameof(input.Description));
            HasMaxLength(input.Description, 50, nameof(input.Description));
            HasLessThenFourPrecision(input.Amount, nameof(input.Amount));
            HasNotNull(input.ExternalId, nameof(input.ExternalId));
            customerValidator.Validate(input.Owner);
            //TODO:can put in more validations

            return true;
        }
    }
        
}
