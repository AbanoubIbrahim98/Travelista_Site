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
    public class RecommendationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecommendationsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recommendations.Include(c => c.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recommendations == null)
            {
                return NotFound();
            }
           

            var city = await _context.Recommendations
                .Include(c => c.City)
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
            ViewData["Country_Id"] = new SelectList(_context.Cities, "ID", "Name");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile? Image_Url, Recommendation recommendation)
        {
            string fileName;
            if (Image_Url == null || !Image_Url.ContentType.StartsWith("image/"))
            {
                ViewData["Country_Id"] = new SelectList(_context.Countries, "ID", "Name");
                ViewBag.image = "Please, just include a photo";
                return View(recommendation);
            }
            if (ModelState.IsValid)
            {
                if (Image_Url != null && Image_Url.ContentType.StartsWith("image/"))
                {

                    fileName = Path.GetFileName(Image_Url.FileName);
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", fileName);
                    using FileStream stream = new FileStream(path, FileMode.Create);
                    Image_Url.CopyTo(stream);
                    recommendation.Image_Url = fileName;
                   
                   
                }
                _context.Recommendations.Add(recommendation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
           
            else
            {
                ViewData["Country_Id"] = new SelectList(_context.Cities, "ID", "Name", recommendation.City_Id);
                return View(recommendation);

            }
           
                
            
               
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recommendations == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendations.FindAsync(id);
            if (recommendation == null)
            {
                return NotFound();
            }
            ViewData["Country_Id"] = new SelectList(_context.Cities, "ID", "Name", recommendation.City_Id);
            return View(recommendation);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile? Image_Url, int id,  Recommendation recommendation)
        {
            if (id != recommendation.ID)
            {
                return NotFound();
            }
            if (Image_Url == null || !Image_Url.ContentType.StartsWith("image/"))
            {
                ViewData["Country_Id"] = new SelectList(_context.Countries, "ID", "Name");
                ViewBag.image = "Please, just include a photo";
                return View(recommendation);
            }
            if (ModelState.IsValid)
            {

                if (Image_Url != null && Image_Url.ContentType.StartsWith("image/"))
                {
                    var fileName = Path.GetFileName(Image_Url.FileName);
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", fileName);
                    using FileStream stream = new FileStream(path, FileMode.Create);
                    Image_Url.CopyTo(stream);
                    recommendation.Image_Url = fileName;
                    
                   
                }
                _context.Update(recommendation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            else
            {

                ViewData["Country_Id"] = new SelectList(_context.Cities, "ID", "Name", recommendation.City_Id);
                return View(recommendation);
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
            if (id == null || _context.Recommendations == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendations
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recommendation == null)
            {
                return NotFound();
            }

            return View(recommendation);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Recommendations'  is null.");
            }
            var recommendation = await _context.Recommendations.FindAsync(id);
            if (recommendation != null)
            {
                _context.Recommendations.Remove(recommendation);
            }

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = "Can't to delete this item";
                return RedirectToAction("Delete", new { id = recommendation.ID });
            }
        }

        private bool CityExists(int id)
        {
          return (_context.Recommendations?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
