using Microsoft.EntityFrameworkCore;
using SomaShareWebApp.Data;
using SomaShareWebApp.Models;
using SomaShareWebApp.Services.Interfaces;
using System.Transactions;

namespace SomaShareWebApp.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _context;

    public ReviewService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetReviewsByUserAsync(string userId)
    {
        return await _context.Reviews
            .Include(r => r.Reviewer)
            .Include(r => r.Transaction)
            .Where(r => r.RevieweeId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetReviewsGivenByUserAsync(string userId)
    {
        return await _context.Reviews
            .Include(r => r.Reviewee)
            .Include(r => r.Transaction)
            .Where(r => r.ReviewerId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<Review> CreateReviewAsync(Review review)
    {
        var transaction = await _context.Transactions.FindAsync(review.TransactionId)
            ?? throw new InvalidOperationException("Transaction not found");

        if (transaction.Status != TransactionStatus.Completed)
        {
            throw new InvalidOperationException("Can only review completed transactions");
        }

        if (transaction.BuyerId != review.ReviewerId && transaction.SellerId != review.ReviewerId)
            throw new UnauthorizedAccessException("You are not part of this transaction");

        // Determine the person who is being reviewed based on reviewer
        review.RevieweeId = transaction.BuyerId == review.ReviewerId
            ? transaction.SellerId
            : transaction.BuyerId;

        // Check if review already exists
        var existingReview = await _context.Reviews
            .FirstOrDefaultAsync(r =>
                r.TransactionId == review.TransactionId &&
                r.ReviewerId == review.ReviewerId);

        if (existingReview != null)
            throw new InvalidOperationException("You have already reviewed this transaction");

        // Validate rating
        if (review.Rating < 1 || review.Rating > 5)
            throw new InvalidOperationException("Rating must be between 1 and 5");

        review.CreatedAt = DateTime.UtcNow;

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        // Update reviewee trust score
        await UpdateUserTrustScoreAsync(review.RevieweeId);

        return review;
    }

    public async Task<bool> CanUserReviewTransactionAsync(int transactionId, string userId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId);
        if (transaction == null || transaction.Status != TransactionStatus.Completed)
            return false;

        if (transaction.BuyerId != userId && transaction.SellerId != userId)
            return false;

        var existingReview = await _context.Reviews
            .AnyAsync(r => r.TransactionId == transactionId && r.ReviewerId == userId);

        return !existingReview;
    }

    public async Task UpdateUserTrustScoreAsync(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return;

        var reviews = await _context.Reviews
            .Where(r => r.RevieweeId == userId)
            .ToListAsync();

        if (reviews.Count == 0)
        {
            user.TrustScore = 0;
        }
        else
        {
            // Calculate weighted average based on recency
            var weightedSum = 0m;
            var weightTotal = 0m;
            var now = DateTime.UtcNow;

            foreach (var review in reviews)
            {
                var ageInDays = (now - review.CreatedAt).TotalDays;
                var weight = Math.Max(0.5m, 1m - (decimal)(ageInDays / 365)); // Recent reviews weighted more
                weightedSum += review.Rating * weight;
                weightTotal += weight;
            }

            user.TrustScore = Math.Round(weightedSum / weightTotal, 2);
        }

        await _context.SaveChangesAsync();
    }
}

