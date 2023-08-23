using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travelista.Data;
using Travelista.Models;

namespace Travelista.Controllers
{

    [Authorize(Roles = "admin")]
    public class ImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ImagesController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Images.Include(i => i.hotel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var images = await _context.Images
                .Include(i => i.hotel)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name");
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile? f1, IFormFile? f2, IFormFile? f3, IFormFile? f4, IFormFile? f5, Images images)
        {
            Images imagesList = new Images();

            if ((f1==null||!f1.ContentType.StartsWith("image/")) &&
               (f2 == null || !f2.ContentType.StartsWith("image/")) &&
               (f3 == null || !f3.ContentType.StartsWith("image/")) &&
                (f4 == null || !f4.ContentType.StartsWith("image/")) &&
                (f5 == null || !f5.ContentType.StartsWith("image/")))
            {
                ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name", images.Hotel_Id);
                ViewBag.image = "Please, just include a photo";
                return View(images);
            }

            if (ModelState.IsValid)
            {
                
                imagesList.Hotel_Id=images.Hotel_Id;
                imagesList.Image1 = GetUrlImage(f1);
                imagesList.Image2 = GetUrlImage(f2);
                imagesList.Image3 = GetUrlImage(f3);
                imagesList.Image4 = GetUrlImage(f4);
                imagesList.Image5 = GetUrlImage(f5);

                _context.Images.Add(imagesList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            } 
      
            ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name", images.Hotel_Id);
            return View(images);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<string> imagesname = new List<string>();
            
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var images = await _context.Images.FindAsync(id);
            if (images == null)
            {
                return NotFound();
            }
           ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name", images.Hotel_Id);
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile? f1, IFormFile? f2, IFormFile? f3, IFormFile? f4, IFormFile? f5, int id, [Bind("ID,Hotel_Id,Image1")] Images images)
        {
            if (id != images.ID)
            {
                return NotFound();
            }
            if ((f1 == null || !f1.ContentType.StartsWith("image/")) &&
               (f2 == null || !f2.ContentType.StartsWith("image/")) &&
               (f3 == null || !f3.ContentType.StartsWith("image/")) &&
                (f4 == null || !f4.ContentType.StartsWith("image/")) &&
                (f5 == null || !f5.ContentType.StartsWith("image/")))
            {
                ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name", images.Hotel_Id);
                ViewBag.image = "Please, just include a photo";
                return View(images);
            }


            if (ModelState.IsValid)
            {

                images.Image1 = GetUrlImage(f1);
                images.Image2 = GetUrlImage(f2);
                images.Image3 = GetUrlImage(f3);
                images.Image4 = GetUrlImage(f4);
                images.Image5 = GetUrlImage(f5);

                    _context.Update(images);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name", images.Hotel_Id);
            return View(images);
        }
        public string GetUrlImage(IFormFile? formFile)
        {
            string fileName=null;
           
            if (formFile!=null && formFile.ContentType.StartsWith("image/"))
            {
                 fileName = Path.GetFileName(formFile.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", fileName);
                using FileStream stream = new FileStream(path, FileMode.Create);
                formFile.CopyTo(stream);
                return fileName;
            }
            else
            {
                return fileName;
            }
            
            

        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var images = await _context.Images
                .Include(i => i.hotel)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Images == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Images'  is null.");
            }
            var images = await _context.Images.FindAsync(id);
            if (images != null)
            {
                _context.Images.Remove(images);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagesExists(int id)
        {
          return (_context.Images?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
