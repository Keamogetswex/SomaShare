using System.Diagnostics;

namespace SomaShareWebApp.ViewModels;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public record TextbookConditionViewModel(int Id, string Description) : ITextbookConditionViewModel
{
    public string Name { get; set; } = string.Empty;

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
