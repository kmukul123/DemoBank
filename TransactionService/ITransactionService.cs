using System.Threading.Tasks;
using TransactionService;

namespace Service
{
    public interface ITransactionService
    {
        public Task<SaveResponse> SaveTransaction(string input);
        Task<GetResponse> getAllTransactions();
        Task<SaveResponse> UpdateTransaction(string input);
    }
}