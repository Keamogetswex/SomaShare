using SomaShareSS3.Models.Enums;

namespace SomaShareSS3.Models;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? MeetupLocation { get; set; }
    public DateTime? MeetupDateTime { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    // Foreign Keys
    public int OfferId { get; set; }
    public string BuyerId { get; set; } = string.Empty;
    public string SellerId { get; set; } = string.Empty;

    // Navi
    public virtual Offer? Offer { get; set; }
    public virtual ApplicationUser? Buyer { get; set; }
    public virtual ApplicationUser? Seller { get; set; }
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public object FinalPrice { get; internal set; }
}
