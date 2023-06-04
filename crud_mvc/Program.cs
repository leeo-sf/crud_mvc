using crud_mvc.Data;
using crud_mvc.Service;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "server=localhost;userid=root;password=root123;database=web_mvc";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));

builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(connectionString, serverVersion));

builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<ProfissaoService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var enUS = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS }
};

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
