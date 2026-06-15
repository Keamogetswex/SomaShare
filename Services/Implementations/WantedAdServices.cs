using Microsoft.EntityFrameworkCore;
using SomaShareWebApp.Data;
using SomaShareWebApp.Models;
using SomaShareWebApp.Services.Interfaces;

namespace SomaShareWebApp.Services;

public class WantedAdService : IWantedAdService
{
    private readonly ApplicationDbContext _context;

    public WantedAdService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WantedAd>> GetAllActiveWantedAdsAsync()
    {
        return await _context.WantedAds
            .Include(w => w.Buyer)
            .Where(w => w.IsActive)
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task<WantedAd?> GetWantedAdByIdAsync(int id)
    {
        return await _context.WantedAds
            .Include(w => w.Buyer)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<WantedAd>> GetWantedAdsByUserAsync(string userId)
    {
        return await _context.WantedAds
            .Where(w => w.BuyerId == userId)
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task<WantedAd> CreateWantedAdAsync(WantedAd wantedAd)
    {
        wantedAd.CreatedAt = DateTime.UtcNow;
        wantedAd.IsActive = true;

        _context.WantedAds.Add(wantedAd);
        await _context.SaveChangesAsync();
        return wantedAd;
    }

    public async Task<WantedAd> UpdateWantedAdAsync(WantedAd wantedAd)
    {
        var existing = await _context.WantedAds.FindAsync(wantedAd.Id)
            ?? throw new InvalidOperationException("Wanted ad not found");

        existing.Title = wantedAd.Title;
        existing.Author = wantedAd.Author;
        existing.ISBN = wantedAd.ISBN;
        existing.CourseCode = wantedAd.CourseCode;
        existing.Description = wantedAd.Description;
        existing.MaxBudget = wantedAd.MaxBudget;
        existing.MinAcceptableCondition = wantedAd.MinAcceptableCondition;
        existing.Campus = wantedAd.Campus;
        existing.IsActive = wantedAd.IsActive;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteWantedAdAsync(int id, string userId)
    {
        var wantedAd = await _context.WantedAds.FindAsync(id);
        if (wantedAd == null || wantedAd.BuyerId != userId)
            return false;

        _context.WantedAds.Remove(wantedAd);
        await _context.SaveChangesAsync();
        return true;
    }
}

