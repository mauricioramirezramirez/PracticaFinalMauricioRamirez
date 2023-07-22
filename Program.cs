using Microsoft.AspNetCore.Cors.Infrastructure;
using PractivaFinalMauricioRamirez.Models;
using PractivaFinalMauricioRamirez.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoConnection>(builder.Configuration.GetSection("MongoSettings"));

builder.Services.AddSingleton<PersonalItemService>();

// The property names' default camel casing should be changed to match the Pascal casing of the CLR object's property names.
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{

    options.AddPolicy("PoliticaCors",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("PoliticaCors");
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

