using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nullability.Guard;

namespace Nullability;

public class Person : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

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
    public Person(string firstName, string lastName)
    {
        NotNull(firstName);
        FirstName = firstName;
    }

    public Person(string fullName)
    {
        
        if (ParseName(fullName, out string? firstName, out string? lastName))
        {
            FirstName = firstName;
        }
    }

    private static bool ParseName(string fullName,
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

    public static Person CreatePresenter()
    {
        Person p = new("Mark")
        {
            FirstName = null! //Not the answer
        };
        //But FirstName is null here!!!!
        return p;
    }


    //public string LastName { get; init; } //or set

    //same as Nullable<DateTime>
    //Just a readonly property
    public DateTime? DateOfBirth { get; }



    //public Person(object? amINull)
    //{
    //    //_ = amINull.Length;

    //    //Whihc of these is best?
    //    if (amINull == null)
    //    { }
    //    if (object.Equals(amINull, null))
    //    { }
    //    if (amINull is null)
    //    { }
    //    if (ReferenceEquals(amINull, null))
    //    { }
    //    if (amINull is not object)
    //    { }
    //    if (amINull is not { })
    //    { }
    //    //if (string.IsNullOrWhiteSpace(amINull))
    //    //{ }

    //    //_ = amINull ?? "";
    //    //_ = amINull?.Length;
    //    //_ = amINull!.Length;


    //    //_ = amINull == null ? true : false;

    //    try
    //    {
    //        _ = amINull.Length;
    //    }
    //    catch(NullReferenceException)
    //    {

    //    }
    //}
}
