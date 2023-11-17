using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheHotels.Data;
using TheHotels.Models;

namespace TheHotels.Controllers
{

    public class ShoppingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingController(ApplicationDbContext context)
        {
            
            _context = context;
        }
        public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();
            return View(hotel);
        }
        public  IActionResult Invoice(int id) 
        {
            Decimal Tax = 15 / 100;
            var rooms = _context.rooms.SingleOrDefault(p => p.Id == id);
            var Invoice = new Invoice()
            {
                IdRoom = rooms.Id,
                IdHotel = rooms.IdHotel,
                IdRoomDetails = rooms.Id,
                Price = (decimal)rooms.Price,
                Total = (decimal)(rooms.Price * 1),
                Discount = 0,
                Tax = (15 / 100),
              //  Net = Tax * rooms.Price * 1,
                DateFrom = DateTime.Now.Date,
                DateInvoice = DateTime.Now.Date,
                DateTo = DateTime.Now.Date,
                UserId = 3
            };
            _context.invoices.Add(Invoice); 
            _context.SaveChanges();
            return View(Invoice);
        }
        public IActionResult Rooms(int id) 
        {
            var rooms=_context.rooms.Where(p=> p.IdHotel == id).ToList();
            return View(rooms);
        }
    }
}
