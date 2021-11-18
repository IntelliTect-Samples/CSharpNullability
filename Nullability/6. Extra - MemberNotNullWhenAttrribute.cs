using System.Diagnostics.CodeAnalysis;

class Foo
{
    [MemberNotNullWhen(true, "Bar")]
    bool GenerateArray => Bar.HasValue;
    public int? Bar { get; set; }
    public void FooBar()
    {
        var data = (GenerateArray) ? new int[Bar.Value] : null;
    }
}

public class FooTest
{
    [Xunit.Fact]
    public void TryThis()
    {
        Foo foo = new() { Bar = 42 };
        foo.FooBar();
    }
}
