using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ActivityManager.Models;

namespace ActivityManager.Data
{
    public class ActivityManagerContext : DbContext
    {
        public ActivityManagerContext (DbContextOptions<ActivityManagerContext> options)
            : base(options)
        {
        }

        public DbSet<ActivityManager.Models.SaActivity> SaActivity { get; set; } = default!;
        public DbSet<ActivityManager.Models.Category> Category { get; set; } = default!;
    }
}
