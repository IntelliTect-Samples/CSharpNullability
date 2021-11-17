using System.ComponentModel;

namespace Nullability;

public class Person
{

    private string _FirstName;
    public string FirstName
    {
        get => _FirstName;
        set
        {
            if (_FirstName != value)
            {
                _FirstName = value;
            }
        }
    }

    public string LastName { get; init; }

    public System.DateTime DateOfBirth { get; }

}
