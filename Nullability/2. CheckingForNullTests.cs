using Xunit;
using System.ComponentModel;

#nullable enable

namespace Nullability;

public class CheckingForNullTests
{

    [Fact]
    public void CheckingForNull()
    {
        string? text = null;

        // 👌 Preferable Null Coalesce
        Assert.Equal("42", text ?? "42");
        Assert.Equal("42", text ??= "42"); // Includes assignment

        // 👎 While readabilty it high, it could
        //    be overridden with custom objects
        //    so potentially suboptimal
        Assert.True(text == null);

        // 👎 Unnecessarily verbose
        Assert.True(ReferenceEquals(text, null));
        Assert.True(Equals(text, null));

        // 👎 Esoteric
        Assert.False(text?.Length == 0);
        Assert.Throws<NullReferenceException>(
                () => _ = text!.Length);

        // 👌 Preferable
        Assert.True(text is null);
    }


    [Fact]
    public void CheckingForNotNullWithNullableValueType()
    {
        int? nullableNumber = 42;

        // 👍 Preferred due to readability
        Assert.True(nullableNumber is not null);

        // Somewhat obscure
        Assert.True(nullableNumber is { });
        Assert.True(nullableNumber is object);
    }

    [Fact]
    public void CheckingForNotNullWithNonNullableValueType()
    {
        int number = 42;

        // Preferred
        // 👌 Triggers an error on useless null check.
        // Assert.True(isNotNull is not null);

        // 👍 Triggers a warning on useless null check 
        Assert.True(number is object);

        // 👎 No warning on useless not-null check.
        Assert.True(number is { });
    }

    [Fact]
    public void CheckingForNotNullWithReferenceType()
    {
        string? isNotNull = "Inigo Montoya";

        // 👌 Preferred
        // 1. It is consistent with value types.
        // 2. Readability is higher.
        Assert.True(isNotNull is not null);

        // 👎 Somewhat obscure
        Assert.True(isNotNull is { });
        Assert.True(isNotNull is object);
    }

    [Fact]
    public void NullForgivenessOperator()
    {
        Assert.Throws<ArgumentNullException>(() => DoStuffWithNullParameterCheck(null!));
    }

    [Theory]
    [InlineData("Inigo Montoya")]
    [InlineData("")]
    public static void DoStuffWithNullParameterCheck(string data)
    {
        // 👍 Preferable
        ArgumentNullException.ThrowIfNull(data);

        // 👎 Somewhat obscure
        _ = data ?? throw new ArgumentNullException(nameof(data));
        if (data is null) throw new ArgumentNullException(nameof(data));

        // Potentially use conditional check.
        if (data is not null)
        {
            //Do stuff
        }
        else
        {
            Assert.Throws<NullReferenceException>(
                () => _ = data!.Length
            );
            Assert.Null(data);
        }
    }


    [Fact]
    public void NullConditionalOperator()
    {
        object? foo = null;

        string? _ = foo?.ToString();
    }



















    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    [Fact]
    public void RaisingAnEvent()
    {
        PropertyChanged(this, new PropertyChangedEventArgs("MyProperty"));
    }
}