using SomaShareSS3.Models.Enums;

namespace SomaShareSS3.Models;

public class Offer
{
    public int Id { get; set; }
    public decimal OfferAmount { get; set; }
    public string? Message { get; set; }
    public OfferStatus Status { get; set; } = OfferStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Foreign Keys
    public int TextbookId { get; set; }
    public string BuyerId { get; set; } = string.Empty;

    // Nav
    public virtual Textbook? Textbook { get; set; }
    public virtual ApplicationUser? Buyer { get; set; }
    public virtual Transaction? Transaction { get; set; }
    public DateTime RespondedAt { get; internal set; }
}
