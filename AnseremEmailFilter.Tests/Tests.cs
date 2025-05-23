using AnseremEmailFilter.Services;
using Xunit;

namespace AnseremEmailFilter.Tests;

public class EmailFilterServiceTests
{
    private readonly EmailFilterService _service = new();

    [Fact]
    public void Adds_Address_When_Matching_Domain_And_No_Exception()
    {
        var to = "q.qweshnikov@batut.com; w.petrov@alfa.com;";
        var copy = "f.patit@buisness.com;";
        var (newTo, newCopy) = _service.Process(to, copy);

        Assert.Equal("q.qweshnikov@batut.com; w.petrov@alfa.com", newTo);
        Assert.Equal("f.patit@buisness.com; v.vladislavovich@alfa.com", newCopy);
    }

    [Fact]
    public void No_Change_When_Exception_Exists()
    {
        var to = "t.kogni@acl.com";
        var copy = "i.ivanov@tbank.ru";
        var (newTo, newCopy) = _service.Process(to, copy);

        Assert.Equal("t.kogni@acl.com", newTo);
        Assert.Equal("i.ivanov@tbank.ru", newCopy);
    }

    [Fact]
    public void Removes_Substitute_When_Exception_Exists()
    {
        var to = "t.kogni@acl.com; i.ivanov@tbank.ru";
        var copy = "e.gras@tbank.ru; t.tbankovich@tbank.ru; v.veronickovna@tbank.ru";
        var (newTo, newCopy) = _service.Process(to, copy);

        Assert.Equal("t.kogni@acl.com; i.ivanov@tbank.ru", newTo);
        Assert.Equal("e.gras@tbank.ru", newCopy);
    }

    [Fact]
    public void No_Change_When_No_Matching_Domains()
    {
        var to = "z.xcy@email.com";
        var copy = "p.rivet@email.com";
        var (newTo, newCopy) = _service.Process(to, copy);

        Assert.Equal("z.xcy@email.com", newTo);
        Assert.Equal("p.rivet@email.com", newCopy);
    }

    [Fact]
    public void Removes_Only_Relevant_Domain_Substitutes()
    {
        var to = "i.ivanov@tbank.ru";
        var copy = "v.vladislavovich@alfa.com; t.tbankovich@tbank.ru";
        var (newTo, newCopy) = _service.Process(to, copy);

        Assert.Equal("i.ivanov@tbank.ru", newTo);
        Assert.Equal("v.vladislavovich@alfa.com", newCopy);
    }

    [Fact]
    public void Skips_Duplicate_Substitution()
    {
        var to = "w.petrov@alfa.com";
        var copy = "v.vladislavovich@alfa.com";
        var (newTo, newCopy) = _service.Process(to, copy);

        Assert.Equal("w.petrov@alfa.com", newTo);
        Assert.Equal("v.vladislavovich@alfa.com", newCopy); 
    }

    [Fact]
    public void Trims_And_Handles_Spaces()
    {
        var to = "   w.petrov@alfa.com  ; ";
        var copy = "  ";
        var (newTo, newCopy) = _service.Process(to, copy);

        Assert.Equal("w.petrov@alfa.com", newTo);
        Assert.Equal("v.vladislavovich@alfa.com", newCopy);
    }
}
