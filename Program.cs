using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pintea_Razvan_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Pintea_Razvan_Lab2Context' not found.");
builder.Services.AddDbContext<LibraryIdentityContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Pintea_Razvan_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Pintea_Razvan_Lab2Context' not found.")));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryIdentityContext>();
builder.Services.AddDbContext<Pintea_Razvan_Lab2Context>(options => options.UseSqlServer(connectionString));


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
