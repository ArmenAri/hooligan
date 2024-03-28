namespace Hooligan.Web.Client.Models;

public class Item
{
    public required string Name { get; init; }
    public required string Icon { get; init; }

    public string Identifier { get; set; } = Identifiers.Discoveries.ToString();
}