using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGetDemo.Web.Models;
using Spaanjaars.FileProviders.Infrastructure.Files;
using Spaanjaars.Toolkit.Email;

namespace NuGetDemo.Web.Controllers;

public class HomeController : Controller
{
  private readonly IMailSender _mailSender;
  private readonly IFileProvider _fileProvider;

  public HomeController(IMailSender mailSender, IFileProvider fileProvider)
  {
    _mailSender = mailSender;
    _fileProvider = fileProvider;
  }

  public async Task<IActionResult> Index()
  {
    var allFiles = (await _fileProvider.GetFilesAsync("Images")).Select(x => new FileDto(x.Name));
    return View(allFiles.ToList());
  }

  [HttpPost]
  public async Task<IActionResult> Upload(IFormFile file)
  {
    if (file == null)
    {
      return RedirectToAction("Index"); // No file selected for upload. Simply display the page again.
    }
    await using var ms = new MemoryStream();
    await file.CopyToAsync(ms);
    var fileBytes = ms.ToArray();
    var name = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
    await _fileProvider.StoreFileAsync("Images", name, fileBytes, false);
    await _mailSender.SendMessageAsync("sender@example.com", "recipient@example.com", "New file uploaded ", BuildBody(file.FileName));

    return RedirectToAction("Index");
  }

  [HttpPost]
  public async Task<IActionResult> Download(string path)
  {
    var file = await _fileProvider.GetFileAsync("Images", path);
    string fileName;
    if (path.Contains('/'))
    {
      fileName = path.Substring(path.LastIndexOf('/') + 1);
    }
    else
    {
      fileName = path;
    }
    return File(file, "application/x-unknown", fileName);
  }

  [HttpPost]
  public async Task<IActionResult> Delete(string path)
  {
    await _fileProvider.DeleteFileAsync("Images", path);
    return RedirectToAction("Index");
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }

  private static string BuildBody(string originalName)
  {
    return $"A new file has been uploaded. Original name: {originalName}";
  }
}