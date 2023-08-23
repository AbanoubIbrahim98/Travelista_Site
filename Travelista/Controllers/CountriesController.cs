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

    [Authorize(Roles ="admin")]
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CountriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var EditMessage = TempData["message"] as string;
            var CreateMessage = TempData["Create"] as string;
            if (!string.IsNullOrEmpty(CreateMessage))
            {
                ViewBag.CreateMessage = CreateMessage;
            }
            if (!string.IsNullOrEmpty(EditMessage))
            {
                ViewBag.EditMessage = EditMessage;
            }
            return _context.Countries != null ? 
                          View(await _context.Countries.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Countries'  is null.");
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile? Image_Url, [Bind("ID,Name,Currency,Population,Image_Url")] Country country)
        {
            string fileName;
            var country1=await _context.Countries.FirstOrDefaultAsync(c=>c.Name.ToLower()==country.Name.ToLower());
            if (country1 != null)
            {
                ViewBag.message = "can't create this because it already exists";
                return View(country);
            }
            if(Image_Url == null || !Image_Url.ContentType.StartsWith("image/"))
            {
                ViewBag.image = "Please, just include a photo";
                return View(country);
            }

            if (ModelState.IsValid)
                {
                    if (Image_Url != null && Image_Url.ContentType.StartsWith("image/"))
                    {
                        fileName = Path.GetFileName(Image_Url.FileName);
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", fileName);
                        using FileStream stream = new FileStream(path, FileMode.Create);
                        Image_Url.CopyTo(stream);
                        country.Image_Url = fileName;

                    }
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
                TempData["Create"] = "the item is create in this table";
                return RedirectToAction(nameof(Index));

            }
                

          

           
               
                return View(country);

            
                
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile? Image_Url, int id,Country country)
        {

            if (id != country.ID)
            {
                return NotFound();
            }
            if (Image_Url == null || !Image_Url.ContentType.StartsWith("image/"))
            {
                ViewBag.image = "Please, just include a photo";
                return View(country);
            }

            if (ModelState.IsValid)
            {
                if (Image_Url != null && Image_Url.ContentType.StartsWith("image/"))
                {
                    var fileName = Path.GetFileName(Image_Url.FileName);
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", fileName);
                    using FileStream stream = new FileStream(path, FileMode.Create);
                    Image_Url.CopyTo(stream);
                    country.Image_Url = fileName;
                    _context.Update(country);

                }
                
                await _context.SaveChangesAsync();
                TempData["message"] = "the item is update in this table";
                return RedirectToAction(nameof(Index));
            }
        
               return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var errorMessage = TempData["message"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Countries'  is null.");
            }
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = "Can't to delete this Country Because it is linked to the city table";
                return RedirectToAction("Delete", new { id = country.ID });
            }
        }

        private bool CountryExists(int id)
        {
          return (_context.Countries?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
