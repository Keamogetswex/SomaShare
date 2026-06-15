using SomaShareWebApp.Models.Enums;

namespace  SomaShareWebApp.ViewModels;

public class OfferEditViewModel
{
    public int Id { get; set; }
    public decimal OfferAmount { get; set; }
    public string? Message { get; set; }
    public OfferStatus Status { get; set; } = OfferStatus.Pending;
    public int TextbookId { get; set; }
    public string BuyerId { get; set; } = string.Empty;
}
