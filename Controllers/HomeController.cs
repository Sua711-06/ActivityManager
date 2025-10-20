using ActivityManager.Models;
using Microsoft.AspNetCore.Mvc;
using ActivityManager.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SaActivityManager.Controllers {
    public class HomeController: Controller {
        private readonly ILogger<HomeController> _logger;

        private readonly ActivityManagerContext _context;

        public HomeController(ILogger<HomeController> logger, ActivityManagerContext context) {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index() {
            var activities = await _context.SaActivity.Include(a => a.Category)
                .ToListAsync();
            return View(activities);
        }

        public async Task<IActionResult> Details(int id) {
            var activity = await _context.SaActivity.Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.SaActivityId == id);
            if (activity == null) {
                return NotFound();
            }
            return View(activity);
        }

        public IActionResult Privacy() {
            return View();
        }
    }
}
