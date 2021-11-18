using System.Runtime.CompilerServices;

namespace Nullability;

public static class Guard
{
    public static string NotNull(string argument,
        [CallerArgumentExpression("argument")]
         string argumentExpression = null!)
        => argument ?? throw new ArgumentNullException(argumentExpression);
}
