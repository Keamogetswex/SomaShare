namespace SomaShareWebApp.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation
    public virtual ICollection<TextbookCategory> TextbookCategories { get; set; } = new List<TextbookCategory>();
}
