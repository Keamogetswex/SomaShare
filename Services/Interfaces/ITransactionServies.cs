using SomaShareWebApp.Models;
using SomaShareWebApp.Models.Enums;

namespace SomaShareWebApp.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(string userId);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> UpdateTransactionStatusAsync(int transactionId, TransactionStatus status, string userId);
        Task<Transaction> CompleteTransactionAsync(int transactionId, string userId);

    }
}
