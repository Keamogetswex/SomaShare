using SomaShareWebApp.Models;
using  SomaShareWebApp.Models.Enums;

namespace SomaShareWebApp.Services.Interfaces
{
    public interface ITextbookService
    {
        Task<IEnumerable<Textbook>> GetAllTextbooksAsync();
        Task<Textbook?> GetTextbookByIdAsync(int id);
        Task<IEnumerable<Textbook>> GetTextbooksBySellerAsync(string sellerId);
        Task<Textbook> CreateTextbookAsync(Textbook textbook);
        Task<Textbook> UpdateTextbookAsync(Textbook textbook);
        Task<bool> DeleteTextbookAsync(int id, string userId);
        Task<(IEnumerable<Textbook> Items, int TotalCount)> SearchTextbooksAsync(
            string? searchTerm = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            TextbookCondition? condition = null,
            string? campus = null,
            string? sortBy = null,
            bool sortDescending = false,
            int page = 1,
            int pageSize = 10);

    }
}
