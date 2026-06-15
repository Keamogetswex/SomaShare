using SomaShareWebApp.Models.Enums;

namespace SomaShareWebApp.ViewModels;

public class TransactionEditViewModel
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? MeetupLocation { get; set; }
    public DateTime? MeetupDateTime { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
    public int OfferId { get; set; }
    public string BuyerId { get; set; } = string.Empty;
    public string SellerId { get; set; } = string.Empty;
}
