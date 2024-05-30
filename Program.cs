using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

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

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
