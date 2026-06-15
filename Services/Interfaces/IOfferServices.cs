using SomaShareWebApp.Models;

namespace SomaShareWebApp.Services.Interfaces
{
    public interface IOfferServices
    {
        Task<Offer?> GetOfferByIdAsync(int id);
        Task<IEnumerable<Offer>> GetOffersByBuyerAsync(string buyerId);
        Task<IEnumerable<Offer>> GetOffersForTextbookAsync(int textbookId);
        Task<IEnumerable<Offer>> GetOffersReceivedBySellerAsync(string sellerId);
        Task<Offer> CreateOfferAsync(Offer offer);
        Task<Offer> AcceptOfferAsync(int offerId, string sellerId);
        Task<Offer> RejectOfferAsync(int offerId, string sellerId);
        Task<Offer> WithdrawOfferAsync(int offerId, string buyerId);

    }
}
