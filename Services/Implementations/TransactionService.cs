using Microsoft.EntityFrameworkCore;
using SomaShareWebApp.Data;
using SomaShareWebApp.Models;
using SomaShareWebApp.Models.Enums;
using SomaShareWebApp.Services.Interfaces;


namespace SomaShareWebApp.Services;

public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _context;
    private readonly IReviewService _reviewService;

    public TransactionService(ApplicationDbContext context, IReviewService reviewService)
    {
        _context = context;
        _reviewService = reviewService;
    }

    public async Task<Transaction?> GetTransactionByIdAsync(int id)
    {
        return await _context.Transactions
            .Include(t => t.Offer)
                .ThenInclude(o => o.Textbook)
            .Include(t => t.Buyer)
            .Include(t => t.Seller)
            .Include(t => t.Reviews)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(string userId)
    {
        return await _context.Transactions
            .Include(t => t.Offer)
                .ThenInclude(o => o.Textbook)
            .Include(t => t.Buyer)
            .Include(t => t.Seller)
            .Include(t => t.Reviews)
            .Where(t => t.BuyerId == userId || t.SellerId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        var offer = await _context.Offers
            .Include(o => o.Textbook)
            .FirstOrDefaultAsync(o => o.Id == transaction.OfferId)
            ?? throw new InvalidOperationException("Offer not found");

        if (offer.Status != OfferStatus.Accepted)
            throw new InvalidOperationException("Can only create transaction for accepted offers");

        // Check if transaction already exists for this offer
        var existingTransaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.OfferId == transaction.OfferId);

        if (existingTransaction != null)
            throw new InvalidOperationException("Transaction already exists for this offer");

        transaction.BuyerId = offer.BuyerId;
        transaction.SellerId = offer.Textbook.SellerId;
        transaction.FinalPrice = offer.OfferAmount;
        transaction.CreatedAt = DateTime.UtcNow;
        transaction.Status = TransactionStatus.Pending;

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> UpdateTransactionStatusAsync(int transactionId, TransactionStatus status, string userId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId)
            ?? throw new InvalidOperationException("Transaction not found");

        if (transaction.BuyerId != userId && transaction.SellerId != userId)
            throw new UnauthorizedAccessException("You are not part of this transaction");

        transaction.Status = status;

        if (status == TransactionStatus.InProgress)
        {
            // Transaction is now active
        }
        else if (status == TransactionStatus.Cancelled)
        {
            // Reactivate the textbook if cancelled
            var offer = await _context.Offers
                .Include(o => o.Textbook)
                .FirstOrDefaultAsync(o => o.Id == transaction.OfferId);

            if (offer != null)
            {
                offer.Textbook.IsAvailable = true;
                offer.Status = OfferStatus.Rejected;
            }
        }

        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> CompleteTransactionAsync(int transactionId, string userId)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Buyer)
            .Include(t => t.Seller)
            .FirstOrDefaultAsync(t => t.Id == transactionId)
            ?? throw new InvalidOperationException("Transaction not found");

        if (transaction.BuyerId != userId && transaction.SellerId != userId)
            throw new UnauthorizedAccessException("You are not part of this transaction");

        if (transaction.Status == TransactionStatus.Completed)
            throw new InvalidOperationException("Transaction is already completed");

        transaction.Status = TransactionStatus.Completed;
        transaction.CompletedAt = DateTime.UtcNow;

        // Update completed transaction counts
        transaction.Buyer.CompletedTransactions++;
        transaction.Seller.CompletedTransactions++;

        await _context.SaveChangesAsync();

        // Update trust scores
        await _reviewService.UpdateUserTrustScoreAsync(transaction.BuyerId);
        await _reviewService.UpdateUserTrustScoreAsync(transaction.SellerId);

        return transaction;
    }
}

public interface ITransactionService
{
}