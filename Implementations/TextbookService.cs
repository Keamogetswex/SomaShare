using Microsoft.EntityFrameworkCore;
using SomaShare.Infrastructure.Data;
using SomaShareSS3.Models;
using SomaShareSS3.Models.Enums;
using SomaShareSS3.Services.Interfaces;

namespace SomaShare.Infrastructure.Services;

public class TextbookService : ITextbookService
{
    private readonly ApplicationDbContext _context;

    public TextbookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Textbook>> GetAllTextbooksAsync()
    {
        return await _context.Textbooks
            .Include(t => t.Seller)
            .Include(t => t.TextbookCategories)
                .ThenInclude(tc => tc.Category)
            .Where(t => t.IsAvailable)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Textbook?> GetTextbookByIdAsync(int id)
    {
        return await _context.Textbooks
            .Include(t => t.Seller)
            .Include(t => t.Offers)
                .ThenInclude(o => o.Buyer)
            .Include(t => t.TextbookCategories)
                .ThenInclude(tc => tc.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Textbook>> GetTextbooksBySellerAsync(string sellerId)
    {
        return await _context.Textbooks
            .Include(t => t.Offers)
            .Where(t => t.SellerId == sellerId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Textbook> CreateTextbookAsync(Textbook textbook)
    {
        textbook.CreatedAt = DateTime.UtcNow;
        _context.Textbooks.Add(textbook);
        await _context.SaveChangesAsync();
        return textbook;
    }

    public async Task<Textbook> UpdateTextbookAsync(Textbook textbook)
    {
        var existing = await _context.Textbooks.FindAsync(textbook.Id)
            ?? throw new InvalidOperationException("Textbook not found");

        existing.Title = textbook.Title;
        existing.Author = textbook.Author;
        existing.ISBN = textbook.ISBN;
        existing.CourseCode = textbook.CourseCode;
        existing.Description = textbook.Description;
        existing.Condition = textbook.Condition;
        existing.Price = textbook.Price;
        existing.Campus = textbook.Campus;
        existing.ImageUrl = textbook.ImageUrl;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteTextbookAsync(int id, string userId)
    {
        var textbook = await _context.Textbooks
            .Include(t => t.Offers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (textbook == null || textbook.SellerId != userId)
            return false;

        // Check for active offers or transactions
        if (textbook.Offers.Any(o => o.Status == OfferStatus.Accepted))
            throw new InvalidOperationException("Cannot delete textbook with accepted offers");

        _context.Textbooks.Remove(textbook);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(IEnumerable<Textbook> Items, int TotalCount)> SearchTextbooksAsync(
        string? searchTerm = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        TextbookCondition? condition = null,
        string? campus = null,
        string? sortBy = null,
        bool sortDescending = false,
        int page = 1,
        int pageSize = 10)
    {
        IQueryable<Textbook> query = _context.Textbooks
            .Include(t => t.Seller)
            .Include(t => t.TextbookCategories)
                .ThenInclude(tc => tc.Category)
            .Where(t => t.IsAvailable);

        // Search filter
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.ToLower();
            query = query.Where(t =>
                t.Title.ToLower().Contains(term) ||
                t.Author.ToLower().Contains(term) ||
                (t.ISBN != null && t.ISBN.Contains(term)) ||
                (t.CourseCode != null && t.CourseCode.ToLower().Contains(term)));
        }

        // Price range filter
        if (minPrice.HasValue)
            query = query.Where(t => t.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(t => t.Price <= maxPrice.Value);

        // Condition filter
        if (condition.HasValue)
            query = query.Where(t => t.Condition == condition.Value);

        // Campus filter
        if (!string.IsNullOrWhiteSpace(campus))
            query = query.Where(t => t.Campus.ToLower().Contains(campus.ToLower()));

        // Get total count before pagination
        var totalCount = await query.CountAsync();

        // Sorting
        query = sortBy?.ToLower() switch
        {
            "price" => sortDescending
                ? query.OrderByDescending(t => t.Price)
                : query.OrderBy(t => t.Price),
            "date" => sortDescending
                ? query.OrderByDescending(t => t.CreatedAt)
                : query.OrderBy(t => t.CreatedAt),
            "title" => sortDescending
                ? query.OrderByDescending(t => t.Title)
                : query.OrderBy(t => t.Title),
            _ => query.OrderByDescending(t => t.CreatedAt)
        };

        // Pagination
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}

