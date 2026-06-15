using SomaShareWebApp.Models;

namespace SomaShareWebApp.Services.Interfaces
{
    public interface IWantedAdService
    {
        Task<IEnumerable<WantedAd>> GetAllActiveWantedAdsAsync();
        Task<WantedAd?> GetWantedAdByIdAsync(int id);
        Task<IEnumerable<WantedAd>> GetWantedAdsByUserAsync(string userId);
        Task<WantedAd> CreateWantedAdAsync(WantedAd wantedAd);
        Task<WantedAd> UpdateWantedAdAsync(WantedAd wantedAd);
        Task<bool> DeleteWantedAdAsync(int id, string userId);
    }
}
