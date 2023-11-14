using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TheHotels.Data;
using TheHotels.Models;

namespace TheHotels.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
       
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult CreateNewRecord(Hotel hotels)
        { 
            if (ModelState.IsValid)
            {
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var hotel = _context.hotel.ToList();
			return RedirectToAction("Index",hotel);
        }
        public IActionResult Update(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Update(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit");
        }
		//begin home work 3
		[HttpGet]
		public IActionResult FilterCity(string filterCity)
		{

			List<Hotel> hotels;

			if (!string.IsNullOrEmpty(filterCity))
			{

				hotels = _context.hotel.Where(h => h.City.ToLower() == filterCity.ToLower()).ToList();

			}
			else
			{

				hotels = _context.hotel.ToList();
			}


			return View("Index", hotels);
		}
		//end home work 3
		public IActionResult Edit(int Id)
        {
            var hoteledit=_context.hotel.SingleOrDefault(x=>x.Id==Id); //search
            return View(hoteledit); //send to view page
        }
        public IActionResult Delete(int Id)
        {
            var hoteldelete = _context.hotel.SingleOrDefault(x=>x.Id== Id); //search
            _context.hotel.Remove(hoteldelete); //delete
            _context.SaveChanges(); //save
            return RedirectToAction("Index");
        }
     
		public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();
            return View(hotel);
        }

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