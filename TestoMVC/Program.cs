using Microsoft.EntityFrameworkCore;
using TestoMVC.Data;
using TestoMVC.Interfaces;
using TestoMVC.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICaseStudiesRepository, CaseStudiesRepository>();
builder.Services.AddScoped<ITestCasesRepository, TestCasesRepository>();
builder.Services.AddScoped<ITestResultsRepository, TestResultsRepository>();
builder.Services.AddDbContext<dbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



var app = builder.Build();

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
    pattern: "{controller=CaseStudy}/{action=Index}/{id?}");

app.Run();
