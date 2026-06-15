using SomaShareWebApp.Models.Enums;

namespace SomaShareWebApp.ViewModels;

public class TextbookListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string? CourseCode { get; set; }
    public string Campus { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TextbookCondition Condition { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
    public string SellerName { get; set; } = string.Empty;
}
