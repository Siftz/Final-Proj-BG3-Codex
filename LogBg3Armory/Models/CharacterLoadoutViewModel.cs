namespace LogBg3Armory.Models;

public class CharacterLoadoutViewModel
{
    public List<Item> WardrobeItems { get; set; } = new();
    
    public Item EquippedHelmet { get; set; }
    public Item EquippedArmour { get; set; }
    public Item EquippedGloves { get; set; }
    public Item EquippedBoots { get; set; }
    public Item EquippedCloak { get; set; }
    public Item EquippedAmulet { get; set; }
    public Item EquippedRing1 { get; set; }
    public Item EquippedRing2 { get; set; }
    
    public Item EquippedMeleeMainhand { get; set; }
    public Item EquippedMeleeOffhand { get; set; }
    public Item EquippedRangedMainhand { get; set; }
    public Item EquippedRangedOffhand { get; set; }
    
}