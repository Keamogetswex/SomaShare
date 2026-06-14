using SomaShareSS3.Models.Enums;

namespace SomaShareSS3.Models;

public class Textbook
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string? ISBN { get; set; }
    public string? CourseCode { get; set; }
    public string? Description { get; set; }
    public TextbookCondition Condition { get; set; }
    public decimal Price { get; set; }
    public string Campus { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Foreign Keys
    public string SellerId { get; set; } = string.Empty;

    // Nav
    public virtual ApplicationUser? Seller { get; set; }
    public virtual ICollection<TextbookCategory> TextbookCategories { get; set; } = new List<TextbookCategory>();
    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
