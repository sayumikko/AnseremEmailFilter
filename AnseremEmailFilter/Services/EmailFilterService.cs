using System.Text.RegularExpressions;

namespace AnseremEmailFilter.Services;

public class EmailFilterService
{
    private static readonly Dictionary<string, List<string>> SubstitutionAddresses = new()
    {
        ["tbank.ru"] = new() { "t.tbankovich@tbank.ru", "v.veronickovna@tbank.ru" },
        ["alfa.com"] = new() { "v.vladislavovich@alfa.com" },
        ["vtb.ru"] = new() { "a.aleksandrov@vtb.ru" }
    };

    private static readonly Dictionary<string, HashSet<string>> ExceptionAddresses = new()
    {
        ["tbank.ru"] = new() { "i.ivanov@tbank.ru" },
        ["alfa.com"] = new() { "s.sergeev@alfa.com", "a.andreev@alfa.com" }
    };

    private static readonly HashSet<string> Domains = new(SubstitutionAddresses.Keys);

    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public (string To, string Copy) Process(string to, string copy)
    {
        var toList = ParseValidEmails(to);
        var copyList = ParseValidEmails(copy);

        var allEmails = toList.Concat(copyList).ToList();
        var matchedDomains = Domains.Where(d => allEmails.Any(a => a.EndsWith("@" + d))).ToList();

        var domainsWithExceptions = matchedDomains
            .Where(domain =>
                ExceptionAddresses.TryGetValue(domain, out var exceptions) &&
                allEmails.Any(addr => exceptions.Contains(addr)))
            .ToList();

        if (domainsWithExceptions.Count > 0)
        {
            foreach (var domain in domainsWithExceptions)
            {
                if (SubstitutionAddresses.TryGetValue(domain, out var substitutes))
                {
                    copyList.RemoveAll(addr => substitutes.Contains(addr));
                }
            }

            return (FormatEmails(toList), FormatEmails(copyList));
        }

        foreach (var domain in matchedDomains)
        {
            if (SubstitutionAddresses.TryGetValue(domain, out var substitutes))
            {
                foreach (var substitute in substitutes)
                {
                    if (!copyList.Contains(substitute) && !allEmails.Contains(substitute))
                    {
                        copyList.Add(substitute);
                    }
                }
            }
        }

        return (FormatEmails(toList), FormatEmails(copyList));
    }

    private List<string> ParseValidEmails(string input)
    {
        return input
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(e => e.Trim())
            .Where(e => EmailRegex.IsMatch(e))
            .ToList();
    }

    private string FormatEmails(List<string> emails) => string.Join("; ", emails);
}
