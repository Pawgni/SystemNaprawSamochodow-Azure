using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SystemRepairCar.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SystemRepairCar.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _context.EventMessages.OrderByDescending(m => m.CreatedAt).ToListAsync();
            return View(messages);
        }
    }
}
