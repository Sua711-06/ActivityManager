using ActivityManager.Data;
using ActivityManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ActivityManager.Controllers {
    public class CategoriesController: Controller {
        private readonly ActivityManagerContext _context;

        public CategoriesController(ActivityManagerContext context) {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index() {
            return View(await _context.Category.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if(category == null) {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Description,R,G,B")] Category category) {
            if(ModelState.IsValid) {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: Categories/CreateAjax
        // Returns JSON so client-side code can update the category select without page reload.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAjax([Bind("CategoryId,Name,Description,R,G,B")] Category category) {
            if (!ModelState.IsValid) {
                // return validation errors as JSON
                var errors = ModelState
                    .Where(kvp => kvp.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                return BadRequest(new { errors });
            }

            _context.Add(category);
            await _context.SaveChangesAsync();

            return Json(new {
                success = true,
                id = category.CategoryId,
                name = category.Name,
                r = category.R,
                g = category.G,
                b = category.B
            });
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if(category == null) {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Description,R,G,B")] Category category) {
            if(id != category.CategoryId) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!CategoryExists(category.CategoryId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if(category == null) {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var category = await _context.Category.FindAsync(id);
            if(category != null) {
                _context.Category.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id) {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
