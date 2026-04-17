using System.Globalization;
using Prestamos.Web.Interfaces;
using Prestamos.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IPrestamoApiService, PrestamoApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7057/");
});

var culture = new CultureInfo("es-DO");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es-DO");
    options.SupportedCultures = new List<CultureInfo> { culture };
    options.SupportedUICultures = new List<CultureInfo> { culture };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Prestamo}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
