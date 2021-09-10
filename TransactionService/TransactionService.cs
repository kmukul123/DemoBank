using DomainModel;
using DomainModel.Validators;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using TransactionService;

namespace Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IValidator<ITransaction> validator;
        private readonly ITransactionRepository transactionRepository;
        private readonly ILogger<TransactionService> logger;
        TransactionVM transaction;

        public TransactionService(IValidator<ITransaction> validator, ITransactionRepository transactionRepository, ILogger<TransactionService> logger)
        {
            this.validator = validator;
            this.transactionRepository = transactionRepository;
            this.logger = logger;
        }

        public async Task<GetResponse> getAllTransactions()
        {

            var result = await transactionRepository
                .GetAllTransactionsAsync();

            return new GetResponse() {
                Body = JsonSerializer.Serialize(result.Select(x => new TransactionVM(x))),
                };
        }

        public async  Task<SaveResponse> SaveTransaction(string input)
        {
            var validationResult = ValidateInput(input);
            if (validationResult != null)
                return validationResult;
            try
            {
                logger.LogInformation($"Saving transaction {transaction.ExternalId}");
                validator.Validate(transaction);
                await transactionRepository.AddTransactionAndCustomerAsync(transaction);
                return new SaveResponse() { Saved = true , ReturnCode = StatusCodes.Status200OK};
            }
            catch (Exception ex)
            {
                logger.LogError($"Error transaction {transaction?.ExternalId} exception {ex.ToString()}");
                return new SaveResponse() { ReturnCode = StatusCodes.Status500InternalServerError };
            }
        }

        public async Task<SaveResponse> UpdateTransaction(string input)
        {
            var validationResult = ValidateInput(input);
            if (validationResult != null)
                return validationResult;

            try
            {
                logger.LogInformation($"Saving transaction {transaction.ExternalId}");

                var ret = await transactionRepository.UpdateTransactionAndCustomerAsync(transaction);
                if (ret < 0)
                    // TOOD review this from security perspective
                    return new SaveResponse() { Saved = false, ReturnCode = StatusCodes.Status404NotFound };

                return new SaveResponse() { Saved = true, ReturnCode = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                logger.LogError($"Error transaction {transaction?.ExternalId} exception {ex.ToString()}");
                return new SaveResponse() { ReturnCode = StatusCodes.Status500InternalServerError };
            }
        }
    

    private SaveResponse ValidateInput(string input)
        {
            try
            {
                logger.LogInformation($"SaveTransaction {input}");
                transaction = JsonSerializer.Deserialize<TransactionVM>(input);
                validator.Validate(transaction);
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error failed to deserialize {input} exception {ex.ToString()}");
                return new SaveResponse() { ReturnCode = StatusCodes.Status400BadRequest };
            }
        }

    }
}
