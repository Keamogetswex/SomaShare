namespace SomaShareSS3.Models;

public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; } // scale 1-5
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public int TransactionId { get; set; }
    public string ReviewerId { get; set; } = string.Empty;
    public string RevieweeId { get; set; } = string.Empty;

    // Nav
    public virtual Transaction? Transaction { get; set; } = null!;
    public virtual ApplicationUser? Reviewer { get; set; } = null!;
    public virtual ApplicationUser? Reviewee { get; set; } = null!;
}
