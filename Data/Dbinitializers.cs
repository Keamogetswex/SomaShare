using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SomaShareWebApp.Models;
using SomaShareWebApp.Models.Enums;

namespace SomaShareWebApp.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        // Ensure database is created
        await context.Database.MigrateAsync();

        // Create roles
        string[] roles = { "Admin", "Seller", "Buyer" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Create default Admin user
        if (await userManager.FindByEmailAsync("admin@university.ac.za") == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@university.ac.za",
                Email = "admin@university.ac.za",
                EmailConfirmed = true,
                FirstName = "System",
                LastName = "Administrator",
                University = "SomaShare University",
                Campus = "Main Campus",
                TrustScore = 5.0m
            };

            var result = await userManager.CreateAsync(admin, "Admin@123!");
            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(admin, new[] { "Admin", "Seller", "Buyer" });
            }
        }

        // sample Seller
        ApplicationUser? seller = await userManager.FindByEmailAsync("kea@university.ac.za");
        if (seller == null)
        {
            seller = new ApplicationUser
            {
                UserName = "kea@university.ac.za",
                Email = "kea@university.ac.za",
                EmailConfirmed = true,
                FirstName = "Kea",
                LastName = "Motswenyane",
                University = "University of STADIO",
                Campus = "Centurion Campus",
                TrustScore = 4.5m,
                CompletedTransactions = 12
            };

            var result = await userManager.CreateAsync(seller, "kea@123!");
            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(seller, new[] { "Seller", "Buyer" });
            }
        }

        //sample Buyer
        ApplicationUser? buyer = await userManager.FindByEmailAsync("melisa@university.ac.za");
        if (buyer == null)
        {
            buyer = new ApplicationUser
            {
                UserName = "melisa@university.ac.za",
                Email = "melisa@university.ac.za",
                EmailConfirmed = true,
                FirstName = "Melisa",
                LastName = "Ndlovu",
                University = "University of STADIO",
                Campus = "Waterfall Campus",
                TrustScore = 4.0m,
                CompletedTransactions = 5
            };

            var result = await userManager.CreateAsync(buyer, "melisa@123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(buyer, "Buyer");
            }
        }

        // Seed Categories
        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new() { Name = "Computer Science", Description = "Programming, algorithms, data structures" },
                new() { Name = "Mathematics", Description = "Calculus, algebra, statistics" },
                new() { Name = "Business", Description = "Accounting, economics, management" },
                new() { Name = "Engineering", Description = "Mechanical, electrical, civil engineering" },
                new() { Name = "Medicine", Description = "Anatomy, pharmacology, clinical studies" },
                new() { Name = "Law", Description = "Constitutional, criminal, commercial law" },
                new() { Name = "Arts & Humanities", Description = "Literature, history, philosophy" },
                new() { Name = "Natural Sciences", Description = "Biology, chemistry, physics" }
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        // Seeding Textbooks
        if (!await context.Textbooks.AnyAsync() && seller != null)
        {
            var categories = await context.Categories.ToListAsync();
            var csCategory = categories.First(c => c.Name == "Computer Science");
            var mathCategory = categories.First(c => c.Name == "Mathematics");
            var businessCategory = categories.First(c => c.Name == "Business");

            var textbooks = new List<Textbook>
            {
                new()
                {
                    Title = "Introduction to Algorithms",
                    Author = "Thomas H. Cormen",
                    ISBN = "9780262033848",
                    CourseCode = "CSC201",
                    Description = "Comprehensive guide to algorithms. Some highlighting but good condition overall.",
                    Condition = TextbookCondition.Good,
                    Price = 850.00m,
                    Campus = "Akoka Campus",
                    SellerId = seller.Id,
                    IsAvailable = true
                },
                new()
                {
                    Title = "Calculus: Early Transcendentals",
                    Author = "James Stewart",
                    ISBN = "9781285741550",
                    CourseCode = "MAT101",
                    Description = "Essential calculus textbook. Like new, barely used.",
                    Condition = TextbookCondition.LikeNew,
                    Price = 650.00m,
                    Campus = "Akoka Campus",
                    SellerId = seller.Id,
                    IsAvailable = true
                },
                new()
                {
                    Title = "Principles of Economics",
                    Author = "N. Gregory Mankiw",
                    ISBN = "9781305585126",
                    CourseCode = "ECO101",
                    Description = "Great introduction to economics. Fair condition with notes.",
                    Condition = TextbookCondition.Fair,
                    Price = 400.00m,
                    Campus = "Akoka Campus",
                    SellerId = seller.Id,
                    IsAvailable = true
                }
            };

            context.Textbooks.AddRange(textbooks);
            await context.SaveChangesAsync();

            // Linking textbooks  to categories
            var savedTextbooks = await context.Textbooks.ToListAsync();
            var textbookCategories = new List<TextbookCategory>
            {
                new() { TextbookId = savedTextbooks[0].Id, CategoryId = csCategory.Id },
                new() { TextbookId = savedTextbooks[1].Id, CategoryId = mathCategory.Id },
                new() { TextbookId = savedTextbooks[2].Id, CategoryId = businessCategory.Id }
            };

            context.TextbookCategories.AddRange(textbookCategories);
            await context.SaveChangesAsync();
        }

        // Seedig Wanted Ads
        if (!await context.WantedAds.AnyAsync() && buyer != null)
        {
            var wantedAds = new List<WantedAd>
            {
                new()
                {
                    Title = "Looking for Database Systems textbook",
                    Author = "Abraham Silberschatz",
                    CourseCode = "CSC301",
                    Description = "Need for upcoming semester. Any condition acceptable.",
                    MaxBudget = 500.00m,
                    MinAcceptableCondition = TextbookCondition.Fair,
                    Campus = "Legon Campus",
                    BuyerId = buyer.Id,
                    IsActive = true
                }
            };

            context.WantedAds.AddRange(wantedAds);
            await context.SaveChangesAsync();
        }
    }
}

