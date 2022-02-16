using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Spaanjaars.Email.Infrastructure;
using Spaanjaars.Email.SendGrid;
using Spaanjaars.Email.Smtp;
using Spaanjaars.FileProviders.FileSystem;
using Spaanjaars.FileProviders.Infrastructure.Files;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Use the local FileSystemFileProvider
builder.Services.AddSingleton<IFileProvider>(new FileSystemFileProvider(builder.Configuration["RootFolder"]));

if (builder.Environment.IsProduction())
{
  builder.Services.AddSingleton<IMailSender, SmtpMailSender>();
}
else
{
  builder.Services.AddEmailToLocalDisk(builder.Configuration.GetSection("MailOnDiskConfiguration"));
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