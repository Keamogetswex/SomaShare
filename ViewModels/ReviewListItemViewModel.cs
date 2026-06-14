namespace SomaShareSS3.ViewModels;

public class ReviewListItemViewModel
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public string RevieweeName { get; set; } = string.Empty;
}
