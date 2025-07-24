namespace LogBg3Armory.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int TypeId { get; set; }
    public ItemType Type { get; set; }

    public int RarityId { get; set; }
    public Rarity Rarity { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; }

    public int ActId { get; set; }
    public Act Act { get; set; }

    public int DescriptionId { get; set; }
    public Description Description { get; set; }
}