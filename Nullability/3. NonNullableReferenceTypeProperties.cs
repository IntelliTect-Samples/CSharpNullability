#nullable enable

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Nullability;

public class Person
{
    public string FirstName { get; init; }

    public string? _LastName;
    public string LastName
    {
        get { return _LastName!; }
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            if (_LastName != value)
            {
                _LastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastName)));
            }
        }
    }

    private string _MiddleName = "";

    [AllowNull]
    public string MiddleName
    {
        get => _MiddleName;
        set => _MiddleName = value ?? "";
    }

    //public Person(string firstName, string lastName)
    //{
    //    // Be sure to check for null
    //    FirstName = firstName;
    //    LastName = lastName;
    //}

    //public Person(string fullName)
    //{

    //    if (TryParseName(fullName, out string? firstName, out string? lastName))
    //    {
    //        FirstName = firstName;
    //        LastName = lastName;
    //    }
    //    else
    //    {
    //        throw new ArgumentException("Full name not valid", nameof(fullName));
    //    }
    //}

    // <See more null code analysis attributes here.
    // https://docs.microsoft.com/dotnet/csharp/language-reference/attributes/nullable-analysis

    private static bool TryParseName(string fullName,
        [NotNullWhen(true)] out string? firstName,
        [NotNullWhen(true)] out string? lastName)
    {
        var parts = fullName.Split(' ');
        if (parts.Length == 2)
        {
            firstName = parts[0];
            lastName = parts[1];
            return true;
        }
        firstName = lastName = null;
        return false;
    }

    //.NET 6
    private DateOnly? _DateOfBirth;

    public event PropertyChangedEventHandler? PropertyChanged;

    public DateOnly? DateOfBirth
    {
        get => _DateOfBirth;
        [MemberNotNull(nameof(Age))]
        init
        {
            if (value is { } dob)
            {
                Age = DateTime.Today.Year - dob.Year;
            }
            else
            {
                throw new ArgumentNullException(nameof(value));
            }
            _DateOfBirth = value;
        }
    }

    public int? Age { get; private set; }
}













/*
    private string? _FirstName;
    public string FirstName
    {
        get => _FirstName!;
        init
        {
            NotNull(value);
            if (_FirstName != value)
            {
                _FirstName = value;

                //var oldWay = PropertyChanged;
                //if (oldWay != null) oldWay(this, new PropertyChangedEventArgs(nameof(FirstName)));

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }
    }
*/