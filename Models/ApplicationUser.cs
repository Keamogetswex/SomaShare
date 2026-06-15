using Microsoft.AspNetCore.Identity;

namespace SomaShareWebApp.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public string Campus { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    public string PreferredLanguage { get; set; } = "en"; // en,zu,tn,etc 

    //  Trust Score (0-5)
    public decimal TrustScore { get; set; } = 0;
    public int CompletedTransactions { get; set; } = 0;

    // Nav Properties
    public virtual ICollection<Textbook> Textbooks { get; set; } = new List<Textbook>();
    public virtual ICollection<WantedAd> WantedAds { get; set; } = new List<WantedAd>();
    public virtual ICollection<Offer> OffersMade { get; set; } = new List<Offer>();
    public virtual ICollection<Transaction> TransactionsAsBuyer { get; set; } = new List<Transaction>();
    public virtual ICollection<Transaction> TransactionsAsSeller { get; set; } = new List<Transaction>();
    public virtual ICollection<Review> ReviewsGiven { get; set; } = new List<Review>();
    public virtual ICollection<Review> ReviewsReceived { get; set; } = new List<Review>();
}
