namespace LogBg3Armory.Models.Builds;

public class ClassBuildDetail
{
    public string ClassName { get; set; }
    public string Playstyle { get; set; }
    public List<string> AbilityProgression { get; set; }
    public List<ItemPathEntry> ItemPath { get; set; }
    public List<LoadoutSlot> FinalLoadout { get; set; }
}

public class ItemPathEntry
{
    public string Name { get; set; }
    public string Location { get; set; }
}

public class LoadoutSlot
{
    public string SlotName { get; set; }
    public string ItemName { get; set; }
}