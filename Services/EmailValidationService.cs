namespace SomaShareWebApp.Services;

public class EmailValidationService
{
    private readonly IConfiguration _configuration;
    private readonly List<string> _allowedDomains;

    public EmailValidationService(IConfiguration configuration)
    {
        _configuration = configuration;
        _allowedDomains = _configuration.GetSection("AllowedEmailDomains").Get<List<string>>()
            ?? new List<string>();
    }

    public bool IsValidUniversityEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        var parts = email.Split('@');
        if (parts.Length != 2)
            return false;

        var domain = parts[1].ToLower();
        return _allowedDomains.Any(d => domain.EndsWith(d.ToLower()));
    }

    public string GetValidationMessage()
    {
        return $"Please use a valid university email address. Accepted domains: {string.Join(", ", _allowedDomains)}";
    }
}

