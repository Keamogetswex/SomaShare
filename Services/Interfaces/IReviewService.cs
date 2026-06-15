using SomaShareWebApp.Models;

namespace SomaShareWebApp.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetReviewsByUserAsync(string userId);
        Task<IEnumerable<Review>> GetReviewsGivenByUserAsync(string userId);
        Task<Review> CreateReviewAsync(Review review);
        Task<bool> CanUserReviewTransactionAsync(int transactionId, string userId);
        Task UpdateUserTrustScoreAsync(string userId);
    }
}
