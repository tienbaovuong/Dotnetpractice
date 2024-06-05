using Auth0.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// Cookie configuration for HTTP to support cookies with SameSite=None
builder.Services.ConfigureSameSiteNoneCookies();

// Cookie configuration for HTTPS
//  builder.Services.Configure<CookiePolicyOptions>(options =>
//  {
//     options.MinimumSameSitePolicy = SameSiteMode.None;
//  });
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

// Add database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RelationDBContext>(options => options.UseSqlServer(connectionString));

// Add services
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ISuburbService, SuburbService>();
builder.Services.AddScoped<IStreetService, StreetService>();
builder.Services.AddScoped<IHouseService, HouseService>();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


// Add swagger document generation
builder.Services.AddSwaggerGen();

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
