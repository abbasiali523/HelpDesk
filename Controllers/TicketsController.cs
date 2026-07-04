using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var isAdmin = User.IsInRole("Admin");

            var tickets = isAdmin
                ? await _context.Tickets.Include(t => t.User).OrderByDescending(t => t.CreatedAt).ToListAsync()
                : await _context.Tickets.Where(t => t.UserId == user.Id).OrderByDescending(t => t.CreatedAt).ToListAsync();

            ViewBag.IsAdmin = isAdmin;
            return View(tickets);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            ModelState.Remove(nameof(Ticket.UserId));
            ModelState.Remove(nameof(Ticket.Status));

            if (!ModelState.IsValid)
                return View(ticket);

            ticket.UserId = user.Id;
            ticket.Status = "باز";
            ticket.CreatedAt = DateTime.Now;

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _context.Tickets.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && ticket.UserId != user!.Id)
                return Forbid();

            ViewBag.IsAdmin = isAdmin;
            return View(ticket);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            var allowed = new[] { "باز", "درحال بررسی", "بسته" };
            if (allowed.Contains(status))
            {
                ticket.Status = status;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
