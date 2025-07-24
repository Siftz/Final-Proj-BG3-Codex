using Microsoft.AspNetCore.Mvc;
using LogBg3Armory.Repositories;
using LogBg3Armory.Models;

namespace LogBg3Armory.Controllers
{
    public class ItemController(ItemRepository repo) : Controller
    {
        public IActionResult Index(string search)
        {
            var items = repo.GetAllItems();

            if (!string.IsNullOrEmpty(search))
                items = items.Where(i => i.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            return View(items);
        }
    }
}