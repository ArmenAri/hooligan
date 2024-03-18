namespace Hooligan.Web.Models;

public class Item
{
    public required string Name { get; init; }
    public required string Icon { get; init; }
}

public class DraggableItem : Item
{
    public string Identifier { get; set; } = Identifiers.Discoveries.ToString();
}