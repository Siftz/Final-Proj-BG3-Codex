using Microsoft.AspNetCore.Mvc;
using LogBg3Armory.Models;
using LogBg3Armory.Extensions;
using LogBg3Armory.Repositories;

namespace LogBg3Armory.Controllers
{
    public class ItemController(ItemRepository repo) : Controller
    {
        // Displays all items with optional search
        /*  public IActionResult Index(string search)
         {
             var items = repo.GetAllItems();

             if (!string.IsNullOrEmpty(search))
                 items = items.Where(i => i.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

             return View(items);
         }*/
        public IActionResult Index(string search)
        {
            var items = repo.GetAllItems();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                items = items.Where(i =>
                    (i.Name != null && i.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (i.Description?.Text != null &&
                     i.Description.Text.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (i.Type?.TypeName != null &&
                     i.Type.TypeName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (i.Rarity?.RarityName != null &&
                     i.Rarity.RarityName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (i.Property?.PropertyName != null &&
                     i.Property.PropertyName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (i.Location?.LocationName != null &&
                     i.Location.LocationName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (i.Act?.ActName != null && i.Act.ActName.Contains(search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            return View(items);
        }


        // Adds an item to the wardrobe stored in session
        public IActionResult AddToWardrobe(int id)
        {
            var item = repo.GetItemById(id);

            if (item != null)
            {
                var wardrobe = HttpContext.Session.GetObjectFromJson<List<Item>>("SelectedItems") ?? new();
                if (!wardrobe.Any(i => i.Id == item.Id))
                {
                    wardrobe.Add(item);
                    HttpContext.Session.SetObjectAsJson("SelectedItems", wardrobe);
                }
            }

            return RedirectToAction("Equipment");
        }

        // Displays wardrobe view
        public IActionResult Wardrobe()
        {
            var selectedItems = HttpContext.Session.GetObjectFromJson<List<Item>>("SelectedItems") ?? new();

            var viewModel = new WardrobeViewModel
            {
                SelectedItems = selectedItems
            };

            return View(viewModel);
        }

        // Loadout Page ViewModel
        public IActionResult Loadout()
        {
            var loadout = HttpContext.Session.GetObjectFromJson<CharacterLoadoutViewModel>("Loadout") ?? new();
            var wardrobe = HttpContext.Session.GetObjectFromJson<List<Item>>("SelectedItems") ?? new();

            var model = new LoadoutPageViewModel
            {
                Loadout = loadout,
                WardrobeItems = wardrobe
            };

            return View(model); // Razor view must be named Loadout.cshtml
        }
        // Build Guides View
        public IActionResult BuildGuides()
        {
            return View("~/Views/Item/BuildGuides/BuildGuides.cshtml");
        }

        public IActionResult CosmicHorror()
        {
            return View("~/Views/Item/BuildGuides/Builds/CosmicHorror.cshtml");
        }
        public IActionResult TavernBrawler()
        {
            return View("~/Views/Item/BuildGuides/Builds/TavernBrawler.cshtml");
        }


        // Removes item from wardrobe
        [HttpPost]
        [HttpPost]
        public IActionResult RemoveFromWardrobe(int id)
        {
            var wardrobe = HttpContext.Session.GetObjectFromJson<List<Item>>("SelectedItems") ?? new();
            wardrobe.RemoveAll(i => i.Id == id);
            HttpContext.Session.SetObjectAsJson("SelectedItems", wardrobe);

            return RedirectToAction("Equipment", "Item");
        }
        // Removes item(s) from character loadout (all slots and just the one specified)
        [HttpPost]
        public IActionResult UnequipItem(int id)
        {
            var loadout = HttpContext.Session.GetObjectFromJson<CharacterLoadoutViewModel>("Loadout") ?? new();

            if (loadout.EquippedHelmet?.Id == id) loadout.EquippedHelmet = null;
            if (loadout.EquippedArmour?.Id == id) loadout.EquippedArmour = null;
            if (loadout.EquippedGloves?.Id == id) loadout.EquippedGloves = null;
            if (loadout.EquippedCloak?.Id == id) loadout.EquippedCloak = null;
            if (loadout.EquippedBoots?.Id == id) loadout.EquippedBoots = null;
            if (loadout.EquippedAmulet?.Id == id) loadout.EquippedAmulet = null;
            if (loadout.EquippedRing1?.Id == id) loadout.EquippedRing1 = null;
            if (loadout.EquippedRing2?.Id == id) loadout.EquippedRing2 = null;
            if (loadout.EquippedMeleeMainhand?.Id == id) loadout.EquippedMeleeMainhand = null;
            if (loadout.EquippedMeleeOffhand?.Id == id) loadout.EquippedMeleeOffhand = null;
            if (loadout.EquippedRangedMainhand?.Id == id) loadout.EquippedRangedMainhand = null;
            if (loadout.EquippedRangedOffhand?.Id == id) loadout.EquippedRangedOffhand = null;

            HttpContext.Session.SetObjectAsJson("Loadout", loadout);
            return RedirectToAction("Equipment");
        }

        [HttpPost]
        public IActionResult UnequipAll()
        {
            var loadout = HttpContext.Session.GetObjectFromJson<CharacterLoadoutViewModel>("Loadout") ?? new();

            loadout.EquippedHelmet = null;
            loadout.EquippedArmour = null;
            loadout.EquippedGloves = null;
            loadout.EquippedCloak = null;
            loadout.EquippedBoots = null;
            loadout.EquippedAmulet = null;
            loadout.EquippedRing1 = null;
            loadout.EquippedRing2 = null;
            loadout.EquippedMeleeMainhand = null;
            loadout.EquippedMeleeOffhand = null;
            loadout.EquippedRangedMainhand = null;
            loadout.EquippedRangedOffhand = null;

            HttpContext.Session.SetObjectAsJson("Loadout", loadout);
            return RedirectToAction("Equipment");
        }

        // Displays equipment screen
        public IActionResult Equipment()
        {
            var wardrobe = HttpContext.Session.GetObjectFromJson<List<Item>>("SelectedItems") ?? new();
            var loadout = HttpContext.Session.GetObjectFromJson<CharacterLoadoutViewModel>("Loadout") ?? new();

            var model = new LoadoutPageViewModel
            {
                Loadout = loadout,
                WardrobeItems = wardrobe
            };

            return View("Loadout", model); // Unified view
        }

        // Equips item to a slot, handling two-handed logic
        [HttpPost]

        // Normalize item slots, this adds the subcategory logic so items are placed correctly (items were not being added correctly without this)
        private string NormalizeSlot(string rawType)
        {
            if (string.IsNullOrWhiteSpace(rawType)) return null;

            var type = rawType.ToLowerInvariant();

            if (type.Contains("helmet")) return "helmet";
            if (type.Contains("glove")) return "gloves";
            if (type.Contains("cloak")) return "cloak";
            if (type.Contains("boot")) return "boots";
            if (type.Contains("amulet")) return "amulet";
            if (type.Contains("ring")) return "ring";

            if (type.Contains("armour") || type.Contains("clothing")) return "armour";

            if (type.Contains("shield") || type.Contains("offhand")) return "melee offhand";

            if (type.Contains("longsword") || type.Contains("mace") || type.Contains("battleaxe") ||
                type.Contains("warhammer") || type.Contains("club") || type.Contains("flail") ||
                type.Contains("maul") || type.Contains("handaxe") || type.Contains("sickle") ||
                type.Contains("greatclub") || type.Contains("morningstar") || type.Contains("rapier") ||
                type.Contains("scimitar") || type.Contains("dagger") || type.Contains("halberd") ||
                type.Contains("glaive") || type.Contains("pike") || type.Contains("spear") ||
                type.Contains("quarterstaff")) return "melee";

            if (type.Contains("bow") || type.Contains("crossbow") || type.Contains("longbow") ||
                type.Contains("shortbow") || type.Contains("hand crossbow") ||
                type.Contains("heavy crossbow")) return "ranged";

            return null;
        }
        // Equips item to a slot, handling two-handed logic
        [HttpPost]
        public IActionResult EquipItem(int itemId)
        {
            // Retrieve wardrobe and loadout from session
            var wardrobe = HttpContext.Session.GetObjectFromJson<List<Item>>("SelectedItems") ?? new();
            var loadout = HttpContext.Session.GetObjectFromJson<CharacterLoadoutViewModel>("Loadout") ?? new();

            // Find the item by ID
            var item = wardrobe.FirstOrDefault(i => i.Id == itemId);
            if (item == null || item.Type?.TypeName == null)
                return RedirectToAction("Loadout");

            // Normalize the item type to a slot category
            string slot = NormalizeSlot(item.Type.TypeName);
            if (slot == null)
                return RedirectToAction("Loadout");

            // Check if item is two-handed
            bool isTwoHanded = item.Property?.PropertyName?.StartsWith("2h") == true;

            // Equip item to appropriate slot
            switch (slot)
            {
                case "helmet":
                    loadout.EquippedHelmet = item;
                    break;
                case "armour":
                    loadout.EquippedArmour = item;
                    break;
                case "gloves":
                    loadout.EquippedGloves = item;
                    break;
                case "cloak":
                    loadout.EquippedCloak = item;
                    break;
                case "boots":
                    loadout.EquippedBoots = item;
                    break;
                case "amulet":
                    loadout.EquippedAmulet = item;
                    break;
                case "ring":
                    if (loadout.EquippedRing1 == null)
                        loadout.EquippedRing1 = item;
                    else
                        loadout.EquippedRing2 = item;
                    break;
                case "melee":
                    if (isTwoHanded)
                    {
                        loadout.EquippedMeleeMainhand = item;
                        loadout.EquippedMeleeOffhand = item;
                    }
                    else if (loadout.EquippedMeleeMainhand == null)
                        loadout.EquippedMeleeMainhand = item;
                    else
                        loadout.EquippedMeleeOffhand = item;

                    break;
                case "ranged":
                    if (isTwoHanded)
                    {
                        loadout.EquippedRangedMainhand = item;
                        loadout.EquippedRangedOffhand = item;
                    }
                    else if (loadout.EquippedRangedMainhand == null)
                        loadout.EquippedRangedMainhand = item;
                    else
                        loadout.EquippedRangedOffhand = item;

                    break;
                case "melee offhand":
                    loadout.EquippedMeleeOffhand = item;
                    break;
            }

            // Save updated loadout back to session
            HttpContext.Session.SetObjectAsJson("Loadout", loadout);

            // Redirect to updated loadout view
            return RedirectToAction("Loadout");
        }
    }
}