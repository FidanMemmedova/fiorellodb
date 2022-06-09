using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEB.DAL;
using WEB.Models;

namespace WEB.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SlideController : Controller
    {
        private AppDbContext _context { get; }
        public SlideController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Slides);
        }
        public IActionResult Create()
        {
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slide.Photo.Length/1024>200)
            {
                ModelState.AddModelError("Photo", "Max size image must be less than 200kb");
                return View();
            }
            if (!slide.Photo.ContentType.Contains("image/"))
            { 
                ModelState.AddModelError("Photo", "Type of file must be image");
                return View();
            }
            //return Json(slide.Photo.ContentType.Contains("image/"));
            //return Json("Ok");
            using (FileStream fileStream = new FileStream(@"C: \Users\ASUS\Desktop\fiorellodb\wwwroot\img" + slide.Photo.FileName, FileMode.Create))
            {
                slide.Photo.CopyTo(fileStream);
            }
            return Json(slide.Photo.FileName);
        }
    }
}
