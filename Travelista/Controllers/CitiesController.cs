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
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CitiesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Cities
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
            var applicationDbContext = _context.Cities.Include(c => c.country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }
           

            var city = await _context.Cities
                .Include(c => c.country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            var errorMessage = TempData["Create"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            ViewData["Country_Id"] = new SelectList(_context.Countries, "ID", "Name");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile? Image_Url, [Bind("ID,Name,Country_Id,Population")] City city)
        {
            string fileName;
            var city1 = await _context.Cities.FirstOrDefaultAsync(c => c.Name == city.Name);
            if(city1 != null)
            {
                ViewData["Country_Id"] = new SelectList(_context.Countries, "ID", "Name");
                ViewBag.message = "This city already exists";
                return View(city);
            }
            if (Image_Url==null|| !Image_Url.ContentType.StartsWith("image/"))
            {
                ViewData["Country_Id"] = new SelectList(_context.Countries, "ID", "Name");
                ViewBag.image = "Please, just include a photo";
                return View(city);
            }
           
                if (ModelState.IsValid)
                {
                    if (Image_Url != null && Image_Url.ContentType.StartsWith("image/"))
                    {
                        fileName = Path.GetFileName(Image_Url.FileName);
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", fileName);
                        using FileStream stream = new FileStream(path, FileMode.Create);
                        Image_Url.CopyTo(stream);
                        city.Image_Url = fileName;


                    }
                    _context.Cities.Add(city);
                    await _context.SaveChangesAsync();
                    TempData["Create"] = "the item is create in this table";
                    return RedirectToAction(nameof(Index));
            }
               
            
                
                ViewData["Country_Id"] = new SelectList(_context.Countries, "ID", "Name");
                return View(city);
   
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["Country_Id"] = new SelectList(_context.Countries, "ID", "Name", city.Country_Id);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile? Image_Url, int id, [Bind("ID,Name,Country_Id,Population,Image_Url")] City city)
        {
            if (id != city.ID)
            {
                return NotFound();
            }
            if (Image_Url == null || !Image_Url.ContentType.StartsWith("image/"))
            {
                ViewData["Country_Id"] = new SelectList(_context.Countries, "ID", "Name");
                ViewBag.image = "Please, just include a photo";
                return View(city);
            }

            if (ModelState.IsValid)
            {

                if (Image_Url!=null&& Image_Url.ContentType.StartsWith("image/"))
                {
                    var fileName = Path.GetFileName(Image_Url.FileName);
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", fileName);
                    using FileStream stream = new FileStream(path, FileMode.Create);
                    Image_Url.CopyTo(stream);
                    city.Image_Url = fileName;
                   
                   
                }
                _context.Update(city);
                await _context.SaveChangesAsync();
                TempData["message"] = "the item is update in this table";
                return RedirectToAction(nameof(Index));
            }
            else
            {

                ViewData["Country_Id"] = new SelectList(_context.Countries, "ID", "Name", city.Country_Id);
                return View(city);
            }
            
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var errorMessage = TempData["message"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cities'  is null.");
            }
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
            }

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = "Can't to delete this item Because it is linked to the hotel table";
                return RedirectToAction("Delete", new { id = city.ID });
            }
        }

        private bool CityExists(int id)
        {
          return (_context.Cities?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
