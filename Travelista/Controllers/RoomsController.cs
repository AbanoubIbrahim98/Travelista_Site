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
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        List<string> RoomType = new List<string>();
        List<string> BedType = new List<string>();
        public RoomsController(ApplicationDbContext context)
        {
           
            RoomType.Add("Single");
            RoomType.Add("Double");
            RoomType.Add("Triple");
            
           
            BedType.Add("Twin");
            BedType.Add("Double");
            BedType.Add("Queen");
            BedType.Add("King");
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rooms.Include(r => r.hotel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.hotel)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            

            ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name");
            ViewData["RoomType"] = new SelectList(RoomType);
            ViewData["BedType"] = new SelectList(BedType);
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Room room)
        {
            var room1=await _context.Rooms.FirstOrDefaultAsync(ro=>ro.Room_Type==room.Room_Type&& ro.Hotel_Id==room.Hotel_Id);
            if (room1 != null)
            {
                ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name", room.Hotel_Id);
                ViewData["RoomType"] = new SelectList(RoomType);
                ViewData["BedType"] = new SelectList(BedType);
                ViewBag.message =" This type of room has been created in this hotel";
                return View(room);
            }
           
            if (ModelState.IsValid)
            {
                if (room.Room_Type == "Single")
                {
                    room.Occupancy = 1;
                }
                else if (room.Room_Type == "Double")
                {
                    room.Occupancy = 2;
                }
                else
                {
                    room.Occupancy = 3;
                }

                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name", room.Hotel_Id);
                ViewData["RoomType"] = new SelectList(RoomType);
                ViewData["BedType"] = new SelectList(BedType);
                return View(room);

            }
               
      
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name", room.Hotel_Id);
            ViewData["RoomType"] = new SelectList(RoomType);
            ViewData["BedType"] = new SelectList(BedType);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.ID)
            {
                return NotFound();
            }
           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.ID))
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
            ViewData["Hotel_Id"] = new SelectList(_context.Hotels, "ID", "Name", room.Hotel_Id);
            ViewData["RoomType"] = new SelectList(RoomType);
            ViewData["BedType"] = new SelectList(BedType);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.hotel)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rooms'  is null.");
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
               _context.Rooms.Remove(room);
            }
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                TempData["message"] = "Can't to delete this item";
                return RedirectToAction("Delete", new { id = room.ID });
            }
           
           
        }

        private bool RoomExists(int id)
        {
          return (_context.Rooms?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
