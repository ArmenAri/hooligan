using System.ComponentModel.DataAnnotations;

namespace Hooligan.Domain;

public class Association
{
    [Required] public string First { get; init; } = string.Empty;
    [Required] public string Second { get; init; } = string.Empty;
    [Required] public string Result { get; init; } = string.Empty;
    [Required] public string Icon { get; init; } = string.Empty;

    public static Association Create(string first, string second, string result, string icon)
    {
        return new Association
        {
            First = first,
            Second = second,
            Result = result,
            Icon = icon
        };
    }
}