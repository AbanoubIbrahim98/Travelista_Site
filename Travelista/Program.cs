using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using System.Configuration;
using Travelista.Data;
using Microsoft.AspNetCore.Authentication.OAuth;
using Travelista.repository.ClassImplmentation;
using Travelista.repository.Interface;

//using Travelista.StripeModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IWeather, WeaherCastA>();
builder.Services.AddScoped<IHotelGet, HotelGetAll>();
builder.Services.AddScoped<IHotelDetails, HotelDetails>();
builder.Services.AddScoped<IBook, BookingRoom>();
builder.Services.AddScoped<IReview, ReviewCord>();
builder.Services.AddScoped<IRecommendation, Recommend>();

builder.Services.AddScoped<BookingSerivce>();
builder.Services.AddScoped<BookingCleanupService>();



//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();

//builder.Services.AddAuthorization(options => options.AddPolicy("admin", policy => policy.RequireRole("admin")));

//////
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        IConfigurationSection googleAuthNSection =
           builder.Configuration.GetSection("Authentication:Google");

        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
    }).AddGitHub(options =>
    {
        options.ClientId = "94b63ce0fe305c4fc55b";
        options.ClientSecret = "95d0d788bb6cec70006366c6eb2d7672ea51bc52";
        options.Scope.Add("user:email");
        options.SaveTokens = true;
    });

///////


builder.Services.AddControllersWithViews();
 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
