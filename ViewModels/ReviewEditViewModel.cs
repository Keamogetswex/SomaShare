namespace SomaShareSS3.ViewModels;

public class ReviewEditViewModel
{
    public int Id { get; set; }
    public int Rating { get; set; } // scale 1-5
    public string Comment { get; set; } = string.Empty;
    public int TransactionId { get; set; }
    public string ReviewerId { get; set; } = string.Empty;
    public string RevieweeId { get; set; } = string.Empty;
}
