using SomaShareSS3.Models.Enums;

namespace SomaShareSS3.ViewModels;

public class OfferListItemViewModel
{
    public int Id { get; set; }
    public string TextbookTitle { get; set; } = string.Empty;
    public decimal OfferAmount { get; set; }
    public string? Message { get; set; }
    public OfferStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string BuyerName { get; set; } = string.Empty;
    public decimal ListingPrice { get; set; }
}
