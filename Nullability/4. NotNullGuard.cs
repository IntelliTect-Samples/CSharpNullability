using System.Runtime.CompilerServices;

namespace Nullability;

public static class Guard
{
    public static T NotNull<T> (T argument,
        [CallerArgumentExpression("argument")]
         string argumentExpression = null!) where T : class
        => argument ?? throw new ArgumentNullException(argumentExpression);
}
