using Microsoft.EntityFrameworkCore;
using SomaShare.Infrastructure.Data;
using SomaShareSS3.Models;
using SomaShareSS3.Models.Enums;

namespace SomaShare.Infrastructure.Services;

public class OfferService : IOfferService
{
    private readonly ApplicationDbContext _context;

    public OfferService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Offer?> GetOfferByIdAsync(int id)
    {
        return await _context.Offers
            .Include(o => o.Textbook)
                .ThenInclude(t => t.Seller)
            .Include(o => o.Buyer)
            .Include(o => o.Transaction)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Offer>> GetOffersByBuyerAsync(string buyerId)
    {
        return await _context.Offers
            .Include(o => o.Textbook)
                .ThenInclude(t => t.Seller)
            .Where(o => o.BuyerId == buyerId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Offer>> GetOffersForTextbookAsync(int textbookId)
    {
        return await _context.Offers
            .Include(o => o.Buyer)
            .Where(o => o.TextbookId == textbookId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Offer>> GetOffersReceivedBySellerAsync(string sellerId)
    {
        return await _context.Offers
            .Include(o => o.Textbook)
            .Include(o => o.Buyer)
            .Where(o => o.Textbook.SellerId == sellerId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<Offer> CreateOfferAsync(Offer offer)
    {
        var textbook = await _context.Textbooks.FindAsync(offer.TextbookId)
            ?? throw new InvalidOperationException("Textbook not found");

        if (!textbook.IsAvailable)
            throw new InvalidOperationException("Textbook is no longer available");

        if (textbook.SellerId == offer.BuyerId)
            throw new InvalidOperationException("Cannot make an offer on your own textbook");

        // Check for existing pending offer from same buyer
        var existingOffer = await _context.Offers
            .FirstOrDefaultAsync(o =>
                o.TextbookId == offer.TextbookId &&
                o.BuyerId == offer.BuyerId &&
                o.Status == OfferStatus.Pending);

        if (existingOffer != null)
            throw new InvalidOperationException("You already have a pending offer for this textbook");

        offer.CreatedAt = DateTime.UtcNow;
        offer.Status = OfferStatus.Pending;

        _context.Offers.Add(offer);
        await _context.SaveChangesAsync();
        return offer;
    }

    public async Task<Offer> AcceptOfferAsync(int offerId, string sellerId)
    {
        var offer = await _context.Offers
            .Include(o => o.Textbook)
            .FirstOrDefaultAsync(o => o.Id == offerId)
            ?? throw new InvalidOperationException("Offer not found");

        if (offer.Textbook.SellerId != sellerId)
            throw new UnauthorizedAccessException("You can only accept offers on your own textbooks");

        if (offer.Status != OfferStatus.Pending)
            throw new InvalidOperationException("Only pending offers can be accepted");

        // Check if another offer was already accepted or not for this specific textbook
        var hasAcceptedOffer = await _context.Offers
            .AnyAsync(o =>
                o.TextbookId == offer.TextbookId &&
                o.Status == OfferStatus.Accepted &&
                o.Id != offerId);

        if (hasAcceptedOffer)
            throw new InvalidOperationException("Another offer has already been accepted for this textbook");

        offer.Status = OfferStatus.Accepted;
        offer.RespondedAt = DateTime.UtcNow;

        // Mark textbook as unavailable if itsn't already
        if (offer.Textbook.IsAvailable)
        {
            offer.Textbook.IsAvailable = false;
        }

        // Reject all other pending offers for this textbook
        var otherOffers = await _context.Offers
            .Where(o => o.TextbookId == offer.TextbookId && o.Status == OfferStatus.Pending && o.Id != offerId)
            .ToListAsync();

        foreach (var otherOffer in otherOffers)
        {
            otherOffer.Status = OfferStatus.Rejected;
            otherOffer.RespondedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return offer;
    }

    public async Task<Offer> RejectOfferAsync(int offerId, string sellerId)
    {
        var offer = await _context.Offers
            .Include(o => o.Textbook)
            .FirstOrDefaultAsync(o => o.Id == offerId)
            ?? throw new InvalidOperationException("Offer not found");

        if (offer.Textbook.SellerId != sellerId)
            throw new UnauthorizedAccessException("You can only reject offers on your own textbooks");

        if (offer.Status != OfferStatus.Pending)
            throw new InvalidOperationException("Only pending offers can be rejected");

        offer.Status = OfferStatus.Rejected;
        offer.RespondedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return offer;
    }

    public async Task<Offer> WithdrawOfferAsync(int offerId, string buyerId)
    {
        var offer = await _context.Offers.FindAsync(offerId)
            ?? throw new InvalidOperationException("Offer not found");

        if (offer.BuyerId != buyerId)
            throw new UnauthorizedAccessException("You can only withdraw your own offers");

        if (offer.Status != OfferStatus.Pending)
            throw new InvalidOperationException("Only pending offers can be withdrawn");

        offer.Status = OfferStatus.Withdrawn;
        offer.RespondedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return offer;
    }
}

public interface IOfferService
{
}