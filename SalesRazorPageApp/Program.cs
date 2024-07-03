using SalesRazorPageApp.Extensions;
using SalesRazorPageApp.Middlewares;
using SalesRazorPageApp.Repositories.Extensions;
using SalesRazorPageApp.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddRazorPages();

builder.Services.AddWebAppDependencies(configuration)
                .AddServicesDependencies()
                .AddRepositoriesDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
