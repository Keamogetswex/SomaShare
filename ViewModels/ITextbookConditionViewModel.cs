namespace SomaShareWebApp.ViewModels
{
    public interface ITextbookConditionViewModel
    {
        string Description { get; init; }
        int Id { get; init; }
        string Name { get; set; }

        void Deconstruct(out int Id, out string Description);
        bool Equals(object? obj);
        bool Equals(TextbookConditionViewModel? other);
        int GetHashCode();
        string ToString();
    }
}