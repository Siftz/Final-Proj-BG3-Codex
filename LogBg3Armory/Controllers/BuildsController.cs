namespace LogBg3Armory.Controllers;

public class BuildsController : Controller
{
    public IActionResult Index()
    {
        var builds = new List<ClassBuildSummary>
        {
            new ClassBuildSummary { ClassName = "Rogue", IconUrl = "/images/icons/rogue.png" },
            new ClassBuildSummary { ClassName = "Paladin", IconUrl = "/images/icons/paladin.png" },
            // Add all 12 classes here
        };

        return View(builds);
    }

    public IActionResult Details(string className)
    {
        var build = BuildRepository.GetBuildByClass(className);
        return View(build);
    }
}