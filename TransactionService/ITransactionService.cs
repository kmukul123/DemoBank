using System.Threading.Tasks;

namespace Service
{
    public interface ITransactionService
    {
        public Task<SaveResponse> SaveTransaction(string input);
    }
}