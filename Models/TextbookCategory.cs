namespace SomaShareSS3.Models;

public class TextbookCategory
{
    // Foreign Keys 
    public int TextbookId { get; set; }
    public int CategoryId { get; set; }

    // Nav
    public virtual Textbook? Textbook { get; set; }
    public virtual Category? Category { get; set; }
}
