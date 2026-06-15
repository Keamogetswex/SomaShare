namespace SomaShareWebApp.ViewModels;

public class WantedAdListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MaxPrice { get; set; }
    public string? PreferredCondition { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string BuyerName { get; set; } = string.Empty;
}
