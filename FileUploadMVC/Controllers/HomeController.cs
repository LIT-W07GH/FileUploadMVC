using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUploadMVC.Data;
using Microsoft.AspNetCore.Mvc;
using FileUploadMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FileUploadMVC.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new ImageUploadRepo(_connectionString);
            var images = repo.GetAll();
            return View(new HomePageViewModel
            {
                Images = images
            });
        }

        [HttpPost]
        public IActionResult Upload(string description, IFormFile image)
        {
            Guid g = Guid.NewGuid();
            var ext = Path.GetExtension(image.FileName);
            var fileName = $"{g}{ext}";
            using (var fileStream = new FileStream($"uploads/{fileName}", FileMode.OpenOrCreate))
            {
                image.CopyTo(fileStream);
            }

            var repo = new ImageUploadRepo(_connectionString);
            repo.Add(new UploadedImage
            {
                Description = description,
                FileName = fileName
            });

            return Redirect("/home/index");
        }
    }
}
