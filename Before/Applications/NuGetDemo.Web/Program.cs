using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spaanjaars.FileProviders.FileSystem;
using Spaanjaars.FileProviders.Infrastructure.Files;
using Spaanjaars.Toolkit.Email;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<List<SmtpSettings>>(options => builder.Configuration.GetSection("SmtpSettings")
  .Bind(options, c => c.BindNonPublicProperties = true));

// Use the local FileSystemFileProvider
builder.Services.AddSingleton<IFileProvider>(new FileSystemFileProvider(builder.Configuration["RootFolder"]));

if (builder.Environment.IsProduction())
{
  builder.Services.AddSmtpServer();
}
else
{
  builder.Services.AddDropLocalSmtpServer(builder.Configuration.GetValue<string>("TempMailFolder"));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
  // To load all attribute routes
  endpoints.MapControllers();
});

app.MapFallbackToFile("index.html");

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();