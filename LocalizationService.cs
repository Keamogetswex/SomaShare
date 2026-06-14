using System.Collections.Concurrent;

namespace SomaShare.Web.Services;

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

    // Fallback to English if available
    if (_translations.TryGetValue("en", out var en) &&
        en.TryGetValue(key, out var enValue))
    {
        return enValue;
    }

    // Last resort: return the key
    return key;
}

public string this[string key] => Translate(key);

private void SeedEnglish()
{
    _translations["en"] = new Dictionary<string, string>
    {
        ["AppName"] = "SomaShare",
        ["Tagline"] = "Share Knowledge, Build Community",
        ["Home"] = "Home",
        ["Browse"] = "Browse Textbooks",
        ["Sell"] = "Sell a Textbook",
        ["WantedAds"] = "Wanted Ads",
        ["Dashboard"] = "My Dashboard",
        ["Login"] = "Login",
        ["Register"] = "Register",
        ["Logout"] = "Logout",
        ["Search"] = "Search",
        ["Filter"] = "Filter",
        ["Price"] = "Price",
        ["Condition"] = "Condition",
        ["Campus"] = "Campus",
        ["MakeOffer"] = "Make an Offer",
        ["AcceptOffer"] = "Accept",
        ["RejectOffer"] = "Reject",
        ["TrustScore"] = "Trust Score",
        ["CompletedTransactions"] = "Completed Transactions",
        ["CashOnMeetup"] = "Cash on Meetup",
        ["MobileMoney"] = "Mobile Money",
        ["MyListings"] = "My Listings",
        ["MyOffers"] = "My Offers",
        ["MyTransactions"] = "My Transactions",
        ["MyReviews"] = "My Reviews",
        ["Save"] = "Save",
        ["Cancel"] = "Cancel",
        ["Delete"] = "Delete",
        ["Edit"] = "Edit",
        ["Confirm"] = "Confirm",
        ["Success"] = "Success",
        ["Error"] = "Error",
        ["Loading"] = "Loading..."
    };
}

private void SeedSwahili()
{
    _translations["sw"] = new Dictionary<string, string>
    {
        ["AppName"] = "SomaShare",
        ["Tagline"] = "Shiriki Maarifa, Jenga Jamii",
        ["Home"] = "Nyumbani",
        ["Browse"] = "Tazama Vitabu",
        ["Sell"] = "Uza Kitabu",
        ["WantedAds"] = "Matangazo Yanayotafutwa",
        ["Dashboard"] = "Dashibodi Yangu",
        ["Login"] = "Ingia",
        ["Register"] = "Jisajili",
        ["Logout"] = "Ondoka",
        ["Search"] = "Tafuta",
        ["Filter"] = "Chuja",
        ["Price"] = "Bei",
        ["Condition"] = "Hali",
        ["Campus"] = "Kampasi",
        ["MakeOffer"] = "Toa Ofa",
        ["AcceptOffer"] = "Kubali",
        ["RejectOffer"] = "Kataa",
        ["TrustScore"] = "Alama ya Uaminifu",
        ["CompletedTransactions"] = "Miamala Iliyokamilika",
        ["CashOnMeetup"] = "Pesa kwa Mkutano",
        ["MobileMoney"] = "Pesa za Simu",
        ["MyListings"] = "Orodha Zangu",
        ["MyOffers"] = "Ofa Zangu",
        ["MyTransactions"] = "Miamala Yangu",
        ["MyReviews"] = "Tathmini Zangu",
        ["Save"] = "Hifadhi",
        ["Cancel"] = "Ghairi",
        ["Delete"] = "Futa",
        ["Edit"] = "Hariri",
        ["Confirm"] = "Thibitisha",
        ["Success"] = "Mafanikio",
        ["Error"] = "Hitilafu",
        ["Loading"] = "Inapakia..."
    };
}

private void SeedZulu()
{
    _translations["zu"] = new Dictionary<string, string>
    {
        ["AppName"] = "SomaShare",
        ["Tagline"] = "Yabelana Ngolwazi, Yakha Umphakathi",
        ["Home"] = "Ikhaya",
        ["Browse"] = "Bheka Izincwadi",
        ["Sell"] = "Thengisa Incwadi",
        ["WantedAds"] = "Izikhangiso Ezifunwayo",
        ["Dashboard"] = "Iphaneli Yami",
        ["Login"] = "Ngena",
        ["Register"] = "Bhalisa",
        ["Logout"] = "Phuma",
        ["Search"] = "Sesha",
        ["Filter"] = "Hlunga",
        ["Price"] = "Intengo",
        ["Condition"] = "Isimo",
        ["Campus"] = "Ikhampasi",
        ["MakeOffer"] = "Yenza I-Offer",
        ["AcceptOffer"] = "Yamukela",
        ["RejectOffer"] = "Yenqaba",
        ["TrustScore"] = "Isilinganiso Sokwethembeka",
        ["CompletedTransactions"] = "Imisebenzi Eseyiqediwe",
        ["CashOnMeetup"] = "Imali Uma Nihlangana",
        ["MobileMoney"] = "Imali Yeselula",
        ["MyListings"] = "Okubhalwe Yimi",
        ["MyOffers"] = "Ama-Offer Ami",
        ["MyTransactions"] = "Imisebenzi Yami",
        ["MyReviews"] = "Ukwahlulelwa Kwami",
        ["Save"] = "Londoloza",
        ["Cancel"] = "Khansela",
        ["Delete"] = "Susa",
        ["Edit"] = "Hlela",
        ["Confirm"] = "Qinisekisa",
        ["Success"] = "Impumelelo",
        ["Error"] = "Iphutha",
        ["Loading"] = "Iyalayisha..."
    };
}

private void SeedXhosa()
{
    _translations["xh"] = new Dictionary<string, string>
    {
        ["AppName"] = "SomaShare",
        ["Tagline"] = "Yabelana Ngolwazi, Yakha Uluntu",
        ["Home"] = "Ekhaya",
        ["Browse"] = "Jonga Iincwadi",
        ["Sell"] = "Thengisa Incwadi",
        ["WantedAds"] = "Izibhengezo Ezifunwayo",
        ["Dashboard"] = "Ideshibhodi Yam",
        ["Login"] = "Ngena",
        ["Register"] = "Bhalisa",
        ["Logout"] = "Phuma",
        ["Search"] = "Khangela",
        ["Filter"] = "Hluza",
        ["Price"] = "Ixabiso",
        ["Condition"] = "Imeko",
        ["Campus"] = "Ikhampasi",
        ["MakeOffer"] = "Yenza Unikezelo",
        ["AcceptOffer"] = "Yamkela",
        ["RejectOffer"] = "Khaba",
        ["TrustScore"] = "Inqanaba Lokuthembeka",
        ["CompletedTransactions"] = "Iintengiselwano ezigqityiweyo",
        ["CashOnMeetup"] = "Imali Xa Nihlangana",
        ["MobileMoney"] = "Imali Yeselula",
        ["MyListings"] = "Uluhlu Lwam",
        ["MyOffers"] = "Izinikezelo Zam",
        ["MyTransactions"] = "Iintengiselwano Zam",
        ["MyReviews"] = "Uphononongo Lwam",
        ["Save"] = "Gcina",
        ["Cancel"] = "Rhoxisa",
        ["Delete"] = "Cima",
        ["Edit"] = "Hlela",
        ["Confirm"] = "Qinisekisa",
        ["Success"] = "Impumelelo",
        ["Error"] = "Imposiso",
        ["Loading"] = "Iyalayisha..."
    };
}

private void SeedTswana()
{
    _translations["tn"] = new Dictionary<string, string>
    {
        ["AppName"] = "SomaShare",
        ["Tagline"] = "Abelana Tsebo, Aga Sechaba",
        ["Home"] = "Gae",
        ["Browse"] = "Lekola Dibuka",
        ["Sell"] = "Rekisa Buka",
        ["WantedAds"] = "Dikitsiso Tsa Go Batlega",
        ["Dashboard"] = "Paneleboto Ya Me",
        ["Login"] = "Tsena",
        ["Register"] = "Ngodisa",
        ["Logout"] = "Tswa",
        ["Search"] = "Batla",
        ["Filter"] = "Sefa",
        ["Price"] = "Theko",
        ["Condition"] = "Maemo",
        ["Campus"] = "Khamphase",
        ["MakeOffer"] = "Dira Tlhahiso",
        ["AcceptOffer"] = "Amogela",
        ["RejectOffer"] = "Gana",
        ["TrustScore"] = "Paloganyo ya Tshepo",
        ["CompletedTransactions"] = "Ditransekšene Tse Feditšwego",
        ["CashOnMeetup"] = "Tebelo Fa Lo Kopana",
        ["MobileMoney"] = "Madi a Founo",
        ["MyListings"] = "Lenane La Me",
        ["MyOffers"] = "Ditlhahiso Tsa Me",
        ["MyTransactions"] = "Ditransekšene Tsa Me",
        ["MyReviews"] = "Ditlhatlhobo Tsa Me",
        ["Save"] = "Boloka",
        ["Cancel"] = "Khansela",
        ["Delete"] = "Phimola",
        ["Edit"] = "Rulaganya",
        ["Confirm"] = "Netefatsa",
        ["Success"] = "Katlego",
        ["Error"] = "Phoso",
        ["Loading"] = "E a laisa..."
    };
}

private void SeedSesotho()
{
    _translations["st"] = new Dictionary<string, string>
    {
        ["AppName"] = "SomaShare",
        ["Tagline"] = "Arolelana Tsebo, Hōla Sechaba",
        ["Home"] = "Lehae",
        ["Browse"] = "Sheba Libuka",
        ["Sell"] = "Rekisa Buka",
        ["WantedAds"] = "Litsebiso Tse Batloang",
        ["Dashboard"] = "Desheboto Ya Ka",
        ["Login"] = "Kena",
        ["Register"] = "Ingolise",
        ["Logout"] = "Tsoa",
        ["Search"] = "Batla",
        ["Filter"] = "Sefahla",
        ["Price"] = "Theko",
        ["Condition"] = "Boemo",
        ["Campus"] = "K’hampase",
        ["MakeOffer"] = "Etsa Nyehelo",
        ["AcceptOffer"] = "Amohela",
        ["RejectOffer"] = "Hana",
        ["TrustScore"] = "Palo ea Tšepo",
        ["CompletedTransactions"] = "Lits’ebelisano Tse Phethiloeng",
        ["CashOnMeetup"] = "Chelete ha Le Kopana",
        ["MobileMoney"] = "Chelete ea Mobi",
        ["MyListings"] = "Lethathamo La Ka",
        ["MyOffers"] = "Linyehelo Tsa Ka",
        ["MyTransactions"] = "Lits’ebelisano Tsa Ka",
        ["MyReviews"] = "Litlhahlobo Tsa Ka",
        ["Save"] = "Boloka",
        ["Cancel"] = "Khansela",
        ["Delete"] = "Phimola",
        ["Edit"] = "Fetola",
        ["Confirm"] = "Netefatsa",
        ["Success"] = "Katleho",
        ["Error"] = "Phoso",
        ["Loading"] = "E a laisa..."
    };
}

private void SeedAfrikaans()
{
    _translations["af"] = new Dictionary<string, string>
    {
        ["AppName"] = "SomaShare",
        ["Tagline"] = "Deel Kennis, Bou Gemeenskap",
        ["Home"] = "Tuis",
        ["Browse"] = "Blaai Boeke",
        ["Sell"] = "Verkoop ’n Boek",
        ["WantedAds"] = "Gesoekte Advertensies",
        ["Dashboard"] = "My Paneelbord",
        ["Login"] = "Meld Aan",
        ["Register"] = "Register",
        ["Logout"] = "Meld Af",
        ["Search"] = "Soek",
        ["Filter"] = "Filter",
        ["Price"] = "Prys",
        ["Condition"] = "Toestand",
        ["Campus"] = "Kampus",
        ["MakeOffer"] = "Maak ’n Aanbod",
        ["AcceptOffer"] = "Aanvaar",
        ["RejectOffer"] = "Verwerp",
        ["TrustScore"] = "Vertrouetelling",
        ["CompletedTransactions"] = "Voltooide Transaksies",
        ["CashOnMeetup"] = "Kontant by Afspraak",
        ["MobileMoney"] = "Mobiele Geld",
        ["MyListings"] = "My Lyste",
        ["MyOffers"] = "My Aanbod",
        ["MyTransactions"] = "My Transaksies",
        ["MyReviews"] = "My Resensies",
        ["Save"] = "Stoor",
        ["Cancel"] = "Kanselleer",
        ["Delete"] = "Vee Uit",
        ["Edit"] = "Wysig",
        ["Confirm"] = "Bevestig",
        ["Success"] = "Sukses",
        ["Error"] = "Fout",
        ["Loading"] = "Laai..."
    };
}

private void SeedXitsonga()
{
    _translations["ts"] = new Dictionary<string, string>
    {
        ["AppName"] = "SomaShare",
        ["Tagline"] = "Avelana Vutshila, Aka Nhlangano",
        ["Home"] = "Ekaya",
        ["Browse"] = "Honisa Tibuku",
        ["Sell"] = "Tengesa Buka",
        ["WantedAds"] = "Switiviso Swa Ku Lava",
        ["Dashboard"] = "Bodini Ra Mina",
        ["Login"] = "Nghena",
        ["Register"] = "Tsarisa",
        ["Logout"] = "Huma",
        ["Search"] = "Sala",
        ["Filter"] = "Fildirha",
        ["Price"] = "Ndzalama",
        ["Condition"] = "Xiyimo",
        ["Campus"] = "Khamphase",
        ["MakeOffer"] = "Endla Nkunguhato",
        ["AcceptOffer"] = "Amukela",
        ["RejectOffer"] = "Arisa",
        ["TrustScore"] = "Nhlayo Ya Ku Tshembeka",
        ["CompletedTransactions"] = "Mintshamisano Leyi Hetisekeke",
        ["CashOnMeetup"] = "Kheshi Loko Mi Hlanganile",
        ["MobileMoney"] = "Mali Ya Foni",
        ["MyListings"] = "Tsalwa Ta Mina",
        ["MyOffers"] = "Minkunguhato Ya Mina",
        ["MyTransactions"] = "Mintshamisano Ya Mina",
        ["MyReviews"] = "Ku Vona Ka Mina",
        ["Save"] = "Hlayisa",
        ["Cancel"] = "Khansela",
        ["Delete"] = "Sula",
        ["Edit"] = "Hlela",
        ["Confirm"] = "Tiyisisa",
        ["Success"] = "Ku Humelela",
        ["Error"] = "Xihoxo",
        ["Loading"] = "Ya layicha..."
    };
}
}
