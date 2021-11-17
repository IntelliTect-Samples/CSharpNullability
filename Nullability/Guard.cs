using System;
using System.Runtime.CompilerServices;

namespace Nullability;

//Guard.NotNull(firstName);
//Gets compiled as
//Guard.NotNull(firstName, "fileName");
public static class Guard
{

    public static string NotNull(string argument,
        [CallerArgumentExpression("argument")]
         string argumentExpression = null!)
        => argument ?? throw new ArgumentNullException(argumentExpression);
}