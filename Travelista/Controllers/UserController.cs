using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Travelista.Controllers

{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<IdentityUser> users=new List<IdentityUser>();
            users = await _userManager.Users.Select(u => u).ToListAsync();    
            
            return View(users);
        }
       

    }
}
