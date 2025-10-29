namespace LogBg3Armory.Repositories;

public static class BuildRepository
{
    public static ClassBuildDetail GetBuildByClass(string className)
    {
        if (className == "Rogue")
        {
            return new ClassBuildDetail
            {
                ClassName = "Rogue",
                Playstyle = "Stealth burst opener",
                AbilityProgression = new List<string> { "Dexterity", "Sharpshooter", "Alert", "Dual Wield" },
                ItemPath = new List<ItemPathEntry>
                {
                    new ItemPathEntry { Name = "Assassin's Shortsword", Location = "Act 2 - Moonrise Towers" },
                    new ItemPathEntry { Name = "Shadow Cloak", Location = "Act 1 - Underdark" }
                },
                FinalLoadout = new List<LoadoutSlot>
                {
                    new LoadoutSlot { SlotName = "Mainhand", ItemName = "Assassin's Shortsword" },
                    new LoadoutSlot { SlotName = "Cloak", ItemName = "Shadow Cloak" }
                }
            };
        }

        return null;
    }
}