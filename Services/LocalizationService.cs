using System.Collections.Concurrent;

namespace SomaShareWebApp.Services
{
    public class LocalizationService
    {
        private readonly ConcurrentDictionary<string, Dictionary<string, string>> _translations;
        private string _currentLanguage = "en"; // default language IS  ENGLISH

        public LocalizationService()
        {
            _translations = new ConcurrentDictionary<string, Dictionary<string, string>>(
                StringComparer.OrdinalIgnoreCase);

            SeedEnglish();
            SeedSwahili();
            SeedZulu();
            SeedXhosa();
            SeedTswana();
            SeedSesotho();
            SeedAfrikaans();
            SeedXitsonga();
        }

        public string CurrentLanguage => _currentLanguage;

        public void SetLanguage(string languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode)) return;
            if (_translations.ContainsKey(languageCode))
            {
                _currentLanguage = languageCode;
            }
        }

        public string Translate(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return key;

            if (_translations.TryGetValue(_currentLanguage, out var dict) &&
                dict.TryGetValue(key, out var value))
            {
                return value;
            }

            // Fallback to English
            if (_translations.TryGetValue("en", out var en) &&
                en.TryGetValue(key, out var enValue))
            {
                return enValue;
            }

            // Last resort: return the key itself
            return key;
        }

        public string this[string key] => Translate(key);

        // Seed methods (English, Swahili, Zulu, Xhosa, Tswana, Sesotho, Afrikaans, Xitsonga)
        private static void SeedEnglish() { /* ... your dictionary ... */ }
        private static void SeedSwahili() { /* ... */ }
        private static void SeedZulu() { /* ... */ }
        private static void SeedXhosa() { /* ... */ }
        private static void SeedTswana() { /* ... */ }
        private static void SeedSesotho() { /* ... */ }
        private static void SeedAfrikaans() { /* ... */ }
        private static void SeedXitsonga() { /* ... */ }
    }
}

