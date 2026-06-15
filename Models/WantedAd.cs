using SomaShareWebApp.Models.Enums;

namespace SomaShareWebApp.Models;

public class WantedAd
{
    internal string Author;
    internal string CourseCode;
    internal decimal MaxBudget;
    internal TextbookCondition MinAcceptableCondition;
    internal string Campus;

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MaxPrice { get; set; }
    public string? PreferredCondition { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    // Foreign Keys
    public string BuyerId { get; set; } = string.Empty;

    // Navi
    public virtual ApplicationUser? Buyer { get; set; } = null!;
    public object ISBN { get; internal set; }
}
