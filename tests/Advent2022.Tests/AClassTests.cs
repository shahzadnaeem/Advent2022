using Advent2022Lib;
using Xunit;

namespace Advent2022Lib.Tests;

public class AClassTests
{
    [Fact]
    public void CanConstuctWithJustName()
    {
        const string Name = "Meee!";

        var calc = new AClass(Name);

        Assert.Equal(Name, calc.Name);
        Assert.True(calc.AutoId);

        var guid = calc.Id;
        Assert.NotEqual(Guid.Empty, guid);
    }

    [Fact]
    public void CanConstructWithId()
    {
        const string Name = "Meee!";
        Guid AGuid = Guid.Empty;

        var calc = new AClass(Name, AGuid);

        Assert.Equal(Name, calc.Name);
        Assert.False(calc.AutoId);

        var guid = calc.Id;
        Assert.Equal(AGuid, guid);
    }

    [Fact]
    public void ToStringTest()
    {
        const string Name = "Meee!";

        var calc = new AClass(Name);

        var toString = calc.ToString();

        Assert.Contains($"Name={Name}", toString);
        Assert.Contains("AutoId=True", toString);
    }
}
