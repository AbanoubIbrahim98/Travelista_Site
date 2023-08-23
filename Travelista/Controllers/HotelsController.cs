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
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            var CreateMessage = TempData["Create"] as string;
            var EditMessage = TempData["Edit"] as string;
            if (!string.IsNullOrEmpty(CreateMessage))
            {
                ViewBag.CreateMessage = CreateMessage;
            }
            if (!string.IsNullOrEmpty(EditMessage))
            {
                ViewBag.EditMessage = EditMessage;
            }
            var applicationDbContext = _context.Hotels.Include(h => h.city).Include(i=>i.images);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.city)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            var CreateMessage = TempData["Create"] as string;

            if (!string.IsNullOrEmpty(CreateMessage))
            {
                ViewBag.CreateMessage = CreateMessage;
            }
            ViewData["City_Id"] = new SelectList(_context.Cities, "ID", "Name");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            var hotel1 = await _context.Hotels.FirstOrDefaultAsync(h => h.Name == hotel.Name && h.City_Id == hotel.City_Id);
            if(hotel1 != null)
            {
                ViewData["City_Id"] = new SelectList(_context.Cities, "ID", "Name", hotel.City_Id);
                ViewBag.messa = " can't create this because it already exists";
                return View(hotel);
            }
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                TempData["Create"] = "this item is create" ;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["City_Id"] = new SelectList(_context.Cities, "ID", "Name", hotel.City_Id);
                TempData["Create"] = "this item can't  create";

                return View(hotel);
            }
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var EditMessage = TempData["Edit"] as string;

            if (!string.IsNullOrEmpty(EditMessage))
            {
                ViewBag.EditMessage = EditMessage;
            }
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["City_Id"] = new SelectList(_context.Cities, "ID", "Name", hotel.City_Id);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Hotel hotel)
        {
            if (id != hotel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                    TempData["Edit"] = "this item is Update";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Edit"] = "this item is Update";
                ViewData["City_Id"] = new SelectList(_context.Cities, "ID", "Name", hotel.City_Id);
                return View(hotel);
            }
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var errorMessage = TempData["message"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.city)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hotels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Hotels'  is null.");
            }
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
            }

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = "Can't to delete this item Because it is linked to the room table";
                return RedirectToAction("Delete", new { id = hotel.ID });
            }
        }

        private bool HotelExists(int id)
        {
          return (_context.Hotels?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
