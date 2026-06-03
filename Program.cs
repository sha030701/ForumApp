using ForumApp.Components;
using ForumApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
  
    builder.Services.AddAuthorization();
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddRazorPages();
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=forum.db"));

 // Lägg till Identity-tjänster
 builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
     .AddRoles<IdentityRole>()
     .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
app.MapRazorPages(); // Behövs för Identity login/register sidor

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
   

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new ForumApp.Models.Category { Name = "Athletics", Description = "Running, Jumping and throwing" },
            new ForumApp.Models.Category { Name = "Swimming", Description = "All swimming events" },
            new ForumApp.Models.Category { Name = "Gymnastics", Description = "Artistic and rhythmic gymnastics" },
            new ForumApp.Models.Category { Name = "Football", Description = "Olympic foorball" }
        );
        db.SaveChanges();
    }
}

//skapa  admin  roll och användare

using (var scope = app.Services.CreateAsyncScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    var adminEmail = "admin@forumapp.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if(adminUser == null)
    {
        adminUser = new IdentityUser {
             UserName = adminEmail,
             Email = adminEmail,
             EmailConfirmed = true};
             await userManager.CreateAsync(adminUser, "Admin123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}
app.Run();
