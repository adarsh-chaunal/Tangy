using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Tangy_DataAccess.Data;
using TangyWeb_Server.Data;
using AutoMapper;
using Tangy_Business.Repository.IRepository;
using Tangy_Business.Repository;
using TangyWeb_Server.Service;
using TangyWeb_Server.Service.IService;
using Syncfusion.Blazor;


Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjczMDU1NkAzMjMzMmUzMDJlMzBHb29MMWw0bW5oQ0lidDlzRHkxejhJSVRvSWFMUXkxVFJ6VmxKeHFPQkhnPQ==");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();

builder.Services.AddScoped<IFileUpload, FileUpload>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage(); 
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub(); // Adding signalR connection for our dotnet application
app.MapFallbackToPage("/_Host"); // if not understand where to route, go to this page. From /_Host we will see routing in action

app.Run();
