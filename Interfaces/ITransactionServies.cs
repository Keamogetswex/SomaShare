using SomaShareSS3.Models;
using SomaShareSS3.Models.Enums;

namespace SomaShareSS3.Services.Interfaces
{
    public interface ITransactionServies
    {
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(string userId);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> UpdateTransactionStatusAsync(int transactionId, TransactionStatus status, string userId);
        Task<Transaction> CompleteTransactionAsync(int transactionId, string userId);

    }
}
