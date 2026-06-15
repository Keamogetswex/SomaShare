using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SomaShareWebApp.Data;
using SomaShareWebApp.Models;
using SomaShareWebApp.Services;
using SomaShareWebApp.Services.Interfaces;
using ITransactionService = SomaShareWebApp.Services.ITransactionService;

namespace SomaShareWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Razor + Blazor
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Database
            builder.Services.AddDbContext<ApplicationDbContext>(optionsAction: options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Authentication
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();

            // Application Services
            builder.Services.AddScoped<ITextbookService, TextbookService>();
            builder.Services.AddScoped<IOfferService, OfferService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IWantedAdService, WantedAdService>();

            // Custom Services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<EmailValidationService>();
            builder.Services.AddScoped<LocalizationService>();

            var app = builder.Build();

            // Database initialization
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    DbInitializer.InitializeAsync(context, userManager, roleManager)
                        .GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                    throw;
                }
            }

            // Pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseAuthentication();
            app.UseAuthorization();

            _ = app.MapRazorComponents<SomaShareWebApp.Blazor.Components.App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }

    internal class Blazor
    {
        internal class Components
        {
            internal class App
            {
            }
        }
    }
}

namespace SomaShareWebApp
{
    class Blazor
    {
        internal class Components
        {
            internal class App
            {
            }
        }
    }
}