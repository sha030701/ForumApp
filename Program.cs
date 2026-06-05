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
  
 builder.Services.ConfigureApplicationCookie(options =>
 {
     options.LoginPath = "/Login"; // Ange sökvägen till inloggningssidan
     options.LogoutPath = "/Logout"; // Ange sökvägen till utloggningssidan
     options.AccessDeniedPath = "/login"; // Ange sökvägen till sidan för åtkomst nekad
 });

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
       // Seed testdata - trådar och kommentarer
    if (!db.ForumThreads.Any())
    {
        db.ForumThreads.AddRange(
            new ForumApp.Models.ForumThread { Title = "100m Sprint – Paris 2024", Content = "Discussion about the 100m sprint results in Paris 2024.", CategoryId = 1, CreatedAt = DateTime.Now.AddDays(-10), UserId = "admin" },
            new ForumApp.Models.ForumThread { Title = "Long Jump Records", Content = "All time long jump records in Olympic history.", CategoryId = 1, CreatedAt = DateTime.Now.AddDays(-8), UserId = "admin" },
            new ForumApp.Models.ForumThread { Title = "Michael Phelps Legacy", Content = "Discussing the greatest swimmer of all time.", CategoryId = 2, CreatedAt = DateTime.Now.AddDays(-7), UserId = "admin" },
            new ForumApp.Models.ForumThread { Title = "4x100m Relay Results", Content = "Results and highlights from the relay races.", CategoryId = 2, CreatedAt = DateTime.Now.AddDays(-5), UserId = "admin" },
            new ForumApp.Models.ForumThread { Title = "Artistic Gymnastics Highlights", Content = "Best moments from artistic gymnastics in Paris 2024.", CategoryId = 3, CreatedAt = DateTime.Now.AddDays(-6), UserId = "admin" },
            new ForumApp.Models.ForumThread { Title = "Olympic Football Final", Content = "Recap of the Olympic football final match.", CategoryId = 4, CreatedAt = DateTime.Now.AddDays(-3), UserId = "admin" }
        );
        db.SaveChanges();


        var threads = db.ForumThreads.ToList();
        db.Comments.AddRange(
            new ForumApp.Models.Comment { Content = "Amazing race! The world record was incredible.", ForumThreadId = threads[0].Id, CreatedAt = DateTime.Now.AddDays(-9), UserId = "admin" },
            new ForumApp.Models.Comment { Content = "I cant believe how fast they ran!", ForumThreadId = threads[0].Id, CreatedAt = DateTime.Now.AddDays(-8), UserId = "admin" },
            new ForumApp.Models.Comment { Content = "Anna is truly the GOAT of swimming.", ForumThreadId = threads[2].Id, CreatedAt = DateTime.Now.AddDays(-6), UserId = "admin" },
            new ForumApp.Models.Comment { Content = "The relay team was absolutely brilliant.", ForumThreadId = threads[3].Id, CreatedAt = DateTime.Now.AddDays(-4), UserId = "admin" },
            new ForumApp.Models.Comment { Content = "Gymnastics is so beautiful to watch.", ForumThreadId = threads[4].Id, CreatedAt = DateTime.Now.AddDays(-5), UserId = "admin" },
            new ForumApp.Models.Comment { Content = "That goal in the final was unbelievable!", ForumThreadId = threads[5].Id, CreatedAt = DateTime.Now.AddDays(-2), UserId = "admin" }
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
