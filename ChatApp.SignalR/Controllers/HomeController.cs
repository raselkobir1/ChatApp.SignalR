using ChatApp.SignalR.Data;
using ChatApp.SignalR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChatApp.SignalR.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;  

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager; 
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currentUser.UserName;
            }
            
            var messages = await _context.Messages.ToListAsync();
            return View(messages);
        }
        public async Task<IActionResult> Create(Message message)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    message.UserName = User.Identity.Name;
                    //var sender = await _userManager.GetUserAsync(User);
                    var sender = await _userManager.GetUserAsync(HttpContext.User);
                    message.UserId = sender.Id;
                    await _context.AddAsync(message);
                    await _context.SaveChangesAsync();
                    return Ok();
               // }
                return Error();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}