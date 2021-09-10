using DomainModel;
using DomainModel.Validators;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IValidator<ITransaction> validator;
        private readonly ITransactionRepository transactionRepository;
        private readonly ILogger<TransactionService> logger;

        public TransactionService(IValidator<ITransaction> validator, ITransactionRepository transactionRepository, ILogger<TransactionService> logger)
        {
            this.validator = validator;
            this.transactionRepository = transactionRepository;
            this.logger = logger;
        }

        public async  Task<SaveResponse> SaveTransaction(string input)
        {
            Transaction transaction;
            try
            {
                logger.LogInformation($"SaveTransaction {input}");
                transaction = JsonSerializer.Deserialize<Transaction>(input);
            } catch (Exception ex) {
                logger.LogError($"Error failed to deserialize {input} exception {ex.ToString()}");
                return new SaveResponse() { ErrorCode = StatusCodes.Status400BadRequest };
            }
            try
            {
                logger.LogInformation($"Saving transaction {transaction.ExternalId}");
                validator.Validate(transaction);
                await transactionRepository.SaveTransactionAndCustomerAsync(transaction);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error transaction {transaction?.ExternalId} exception {ex.ToString()}");
                return new SaveResponse() { ErrorCode = StatusCodes.Status500InternalServerError } ;
            }
            return new SaveResponse() { Saved = true };
        }
    }
}
