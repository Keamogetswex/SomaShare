using SomaShareWebApp.Models.Enums;

namespace SomaShareWebApp.ViewModels;

public class TextbookDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string? ISBN { get; set; }
    public string? CourseCode { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public TextbookCondition Condition { get; set; }
    public string Campus { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public string SellerName { get; set; } = string.Empty;
    public string SellerId { get; set; } = string.Empty;
    public decimal SellerTrustScore { get; set; }
}
