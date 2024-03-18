namespace Hooligan.Common.Extensions;

public static class StringExtensions
{
    public static string ToUpperOnlyFirstCharacterInvariant(this string input)
    {
        return input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(
                input[0].ToString().ToUpperInvariant(),
                input.AsSpan(1).ToString().ToLowerInvariant())
        };
    }
}