using ActivityManager.Data;
using ActivityManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ActivityManager.Controllers {
    public class SaActivitiesController: Controller {
        private readonly ActivityManagerContext _context;

        public SaActivitiesController(ActivityManagerContext context) {
            _context = context;
        }

        // GET: SaActivities
        public async Task<IActionResult> Index() {
            var activityManagerContext = _context.SaActivity.Include(s => s.Category);
            return View(await activityManagerContext.ToListAsync());
        }

        // GET: SaActivities/Details/5
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var saActivity = await _context.SaActivity
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SaActivityId == id);
            if(saActivity == null) {
                return NotFound();
            }

            return View(saActivity);
        }

        // GET: SaActivities/Create
        public IActionResult Create(int? newCategoryId) {
            ViewBag.CategoryId = new SelectList(_context.Category.OrderBy(c => c.Name).ToList(), "CategoryId", "Name", newCategoryId);
            return View();
        }

        // POST: SaActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaActivityId,Name,Description,Location,Date,Created,CategoryId,ImageFile")] SaActivity saActivity) {
            if(ModelState.IsValid) {
                saActivity.Created = DateTime.Now;
                var category = await _context.Category
                    .Include(c => c.Activities)
                    .FirstOrDefaultAsync(c => c.CategoryId == saActivity.CategoryId);
                category?.Activities.Add(saActivity);
                
                if (saActivity.ImageFile != null) {
                    var fileName = Path.GetFileName(saActivity.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create)) {
                        await saActivity.ImageFile.CopyToAsync(stream);
                    }
                    saActivity.ImageFileName = fileName;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", saActivity.CategoryId);
            return View(saActivity);
        }


        // GET: SaActivities/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var saActivity = await _context.SaActivity.FindAsync(id);
            if(saActivity == null) {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", saActivity.CategoryId);
            return View(saActivity);
        }

        // POST: SaActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaActivityId,Name,Description,Location,Date,Created,CategoryId")] SaActivity saActivity) {
            if(id != saActivity.SaActivityId) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    _context.Update(saActivity);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!SaActivityExists(saActivity.SaActivityId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", saActivity.CategoryId);
            return View(saActivity);
        }

        // GET: SaActivities/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var saActivity = await _context.SaActivity
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SaActivityId == id);
            if(saActivity == null) {
                return NotFound();
            }

            return View(saActivity);
        }

        // POST: SaActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var saActivity = await _context.SaActivity.FindAsync(id);
            if(saActivity != null) {
                _context.SaActivity.Remove(saActivity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool SaActivityExists(int id) {
            return _context.SaActivity.Any(e => e.SaActivityId == id);
        }
    }
}
