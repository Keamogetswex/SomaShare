namespace SomaShareSS3.ViewModels;

public class WantedAdEditViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MaxPrice { get; set; }
    public string? PreferredCondition { get; set; }
    public bool IsActive { get; set; } = true;
    public string BuyerId { get; set; } = string.Empty;
}
