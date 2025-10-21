using System.Collections.Generic;

namespace LogBg3Armory.Models
{
    public class LoadoutPageViewModel
    {
        public CharacterLoadoutViewModel Loadout { get; set; }
        public List<Item> WardrobeItems { get; set; }
    }
}