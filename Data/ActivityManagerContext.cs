using Microsoft.EntityFrameworkCore;

namespace ActivityManager.Data {
    public class ActivityManagerContext: DbContext {
        public ActivityManagerContext(DbContextOptions<ActivityManagerContext> options)
            : base(options) {
        }

        public DbSet<ActivityManager.Models.SaActivity> SaActivity { get; set; } = default!;
        public DbSet<ActivityManager.Models.Category> Category { get; set; } = default!;
    }
}
