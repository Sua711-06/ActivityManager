using ActivityManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace SaActivityManager.Controllers {
    public class HomeController: Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            List<SaActivity> activities = new List<SaActivity>
            {
                new SaActivity
                {
                    SaActivityId = 1,
                    Name = "Morning Jog",
                    Description = "A refreshing jog in the park.",
                    Location = "Central Park",
                    Date = DateTime.Now.AddDays(1).AddHours(6),
                    Category = new Category
                    {
                        CategoryId = 1,
                        Name = "Fitness",
                        Description = "Activities related to physical fitness.",
                        CategoryColor = new Category.Color { R = 0, G = 255, B = 0 }
                    }
                },
                new SaActivity
                {
                    SaActivityId = 2,
                    Name = "Team Meeting",
                    Description = "Weekly sync-up with the team.",
                    Location = "Office Conference Room",
                    Date = DateTime.Now.AddDays(1).AddHours(9),
                    Category = new Category
                    {
                        CategoryId = 2,
                        Name = "Work",
                        Description = "Professional work-related activities.",
                        CategoryColor = new Category.Color { R = 0, G = 0, B = 255 }
                    }
                },
                new SaActivity
                {
                    SaActivityId = 3,
                    Name = "Guitar Practice",
                    Description = "Practice session for the upcoming gig.",
                    Location = "Home Studio",
                    Date = DateTime.Now.AddDays(1).AddHours(18),
                    Category = new Category
                    {
                        CategoryId = 3,
                        Name = "Hobby",
                        Description = "Leisure and hobby activities.",
                        CategoryColor = new Category.Color { R = 255, G = 0, B = 0 }
                    }
                }
            };
            return View(activities);
        }

        public IActionResult Details(int id) {
            var activity = new SaActivity {
                SaActivityId = id,
                Name = "Sample Activity",
                Description = "This is a detailed description of the activity.",
                Location = "Sample Location",
                Date = DateTime.Now.AddDays(1),
                Category = new Category {
                    CategoryId = 1,
                    Name = "Sample Category",
                    Description = "This is a sample category.",
                    CategoryColor = new Category.Color { R = 100, G = 150, B = 200 }
                }
            };
            return View(activity);
        }

        public IActionResult Privacy() {
            return View();
        }
    }
}
