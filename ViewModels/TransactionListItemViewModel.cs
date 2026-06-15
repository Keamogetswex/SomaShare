using SomaShareWebApp.Models.Enums;

namespace SomaShareWebApp.ViewModels;

public class TransactionListItemViewModel
{
    public int Id { get; set; }
    public string TextbookTitle { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public TransactionStatus Status { get; set; }
    public string BuyerName { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
