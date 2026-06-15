using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SomaShareWebApp.Models;

namespace SomaShareWebApp.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Textbook> Textbooks => Set<Textbook>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<TextbookCategory> TextbookCategories => Set<TextbookCategory>();
    public DbSet<WantedAd> WantedAds => Set<WantedAd>();
    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuration of user properties, Textbook entity, and relationships 
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.University).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Campus).HasMaxLength(100).IsRequired();
            entity.Property(e => e.TrustScore).HasPrecision(3, 2);
        });
        // TXTBOOK
        builder.Entity<Textbook>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(300).IsRequired();
            entity.Property(e => e.Author).HasMaxLength(200).IsRequired();
            entity.Property(e => e.ISBN).HasMaxLength(20);
            entity.Property(e => e.CourseCode).HasMaxLength(20);
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.Campus).HasMaxLength(100).IsRequired();

            entity.HasIndex(e => e.Title);
            entity.HasIndex(e => e.ISBN);
            entity.HasIndex(e => e.CourseCode);
            entity.HasIndex(e => e.Campus);
            entity.HasIndex(e => e.IsAvailable);

            entity.HasOne(e => e.Seller)
                .WithMany(u => u.Textbooks)
                .HasForeignKey(e => e.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        //  CATEGORY
        builder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // TEXTBOOK Category (M-TO-M RELATIONSHIP Table)
        builder.Entity<TextbookCategory>(entity =>
        {
            entity.HasKey(tc => new { tc.TextbookId, tc.CategoryId });

            entity.HasOne(tc => tc.Textbook)
                .WithMany(t => t.TextbookCategories)
                .HasForeignKey(tc => tc.TextbookId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(tc => tc.Category)
                .WithMany(c => c.TextbookCategories)
                .HasForeignKey(tc => tc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // WANTED AD 
        builder.Entity<WantedAd>(static entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(300).IsRequired();
            entity.Property(e => e.MaxBudget).HasPrecision(10, 2);
            entity.Property(e => e.Campus).HasMaxLength(100).IsRequired();

            entity.HasOne(e => e.Buyer)
                .WithMany(u => u.WantedAds)
                .HasForeignKey(e => e.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Offer Configuration
        builder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OfferAmount).HasPrecision(10, 2);

            entity.HasOne(e => e.Textbook)
                .WithMany(t => t.Offers)
                .HasForeignKey(e => e.TextbookId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Buyer)
                .WithMany(u => u.OffersMade)
                .HasForeignKey(e => e.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Transaction Configuration
        builder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FinalPrice).HasPrecision(10, 2);
            entity.Property(e => e.MeetupLocation).HasMaxLength(300);

            entity.HasOne(e => e.Offer)
                .WithOne(o => o.Transaction)
                .HasForeignKey<Transaction>(e => e.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Buyer)
                .WithMany(u => u.TransactionsAsBuyer)
                .HasForeignKey(e => e.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Seller)
                .WithMany(u => u.TransactionsAsSeller)
                .HasForeignKey(e => e.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // REVIEWS
        builder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Rating).IsRequired();
            entity.Property(e => e.Comment).HasMaxLength(1000);

            entity.HasOne(e => e.Transaction)
                .WithMany(t => t.Reviews)
                .HasForeignKey(e => e.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Reviewer)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(e => e.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Reviewee)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(e => e.RevieweeId)
                .OnDelete(DeleteBehavior.Restrict);

            // VALIDATON OF ONE USER REVIEWING ANOTHER USER ONLY ONCE PER TRANSACTION
            entity.HasIndex(e => new { e.TransactionId, e.ReviewerId }).IsUnique();
        });
    }
}

