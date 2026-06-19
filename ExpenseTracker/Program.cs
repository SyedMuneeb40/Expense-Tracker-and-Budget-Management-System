using Microsoft.EntityFrameworkCore; // Yeh line zaroori hai
using ExpenseTracker.Models;


var builder = WebApplication.CreateBuilder(args);
// Add this inside Program.cs
builder.Services.AddDbContext<ExpenseTracker.Models.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// --- YAHAN SE COPY KAREIN ---
using (var scope = app.Services.CreateScope())
{
var context = scope.ServiceProvider.GetRequiredService<ExpenseTracker.Models.ApplicationDbContext>();

// Agar Categories table khali hai, to yeh data daal do
if (!context.Categories.Any())
{
context.Categories.AddRange(
    new ExpenseTracker.Models.Category { Title = "Salary", Icon = "💰", Type = "Income" },
    new ExpenseTracker.Models.Category { Title = "Business", Icon = "💵", Type = "Income" },
    new ExpenseTracker.Models.Category { Title = "Groceries", Icon = "🥦", Type = "Expense" },
    new ExpenseTracker.Models.Category { Title = "Rent", Icon = "🏠", Type = "Expense" },
    new ExpenseTracker.Models.Category { Title = "Transport", Icon = "🚗", Type = "Expense" },
    new ExpenseTracker.Models.Category { Title = "Fun", Icon = "🎮", Type = "Expense" }
);
context.SaveChanges();
}
}
// --- YAHAN TAK ---

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    // ... baaki code wesa hi rahega

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
