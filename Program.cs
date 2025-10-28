using ActivityManager.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ActivityManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ActivityManagerContext") ?? throw new InvalidOperationException("Connection string 'ActivityManagerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
if(builder.Environment.IsDevelopment()) {
    builder.Configuration.AddUserSecrets<Program>();
}
var app = builder.Build();

// Correct middleware ordering
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Only redirect the exact root path — prevents redirect-to-self loops.
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value ?? string.Empty;
    if (string.Equals(path, "/", StringComparison.Ordinal))
    {
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            context.Response.Redirect("/Home/Index");
            return;
        }
        else
        {
            context.Response.Redirect("/Account/Login");
            return;
        }
    }

    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
