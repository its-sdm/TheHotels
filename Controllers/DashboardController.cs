using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheHotels.Data;
using TheHotels.Models;

namespace TheHotels.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
		//begin home work 3
		[HttpPost]
        public IActionResult Index(string city)
        {
            var hotel = _context.hotel.Where(x=>x.City.Equals(city));
            return View(hotel);
        }

        //end home work 3
        public IActionResult Delete(int Id)
		{
			var hoteldelete = _context.hotel.SingleOrDefault(x => x.Id == Id); //search
            if(hoteldelete != null)
            {
				_context.hotel.Remove(hoteldelete); //delete
				_context.SaveChanges(); //save
                TempData["Del"] = "Ok";
			}
			return RedirectToAction("Index");
		}

		public IActionResult CreateNewRoom(Rooms rooms)
		{

			_context.rooms.Add(rooms);
			_context.SaveChanges();
			return RedirectToAction("Rooms");


		}

		//from me
		//public IActionResult CreateNewRoom(Rooms rooms)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		_context.rooms.Add(rooms);
		//		_context.SaveChanges();
		//		return RedirectToAction("Rooms");
		//	}
		//	else
		//	{
		//		var roomsList = _context.rooms.ToList();
		//		return View("Rooms", roomsList);
		//	}
		//}




		//from me
		//public IActionResult AddRoomDetails(RoomDetails roomDetails)
		//{
		//		_context.roomDetails.Add(roomDetails);
		//		_context.SaveChanges();
		//		return RedirectToAction("RoomDetails");	
		//}

		public IActionResult AddRoomDetails(RoomDetails roomDetails)
		{
			if (ModelState.IsValid)
			{
				_context.roomDetails.Add(roomDetails);
				_context.SaveChanges();
				return RedirectToAction("RoomDetails");
			}

			var roomDetailsList=_context.roomDetails.ToList();
			return View("RoomDetails", roomDetailsList);
		}
		// begin hw 4

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
        public IActionResult Edit(int Id)
        {
            var hoteledit = _context.hotel.SingleOrDefault(x => x.Id == Id); //search
            return View(hoteledit); //send to view page
        }
        //end hw 4
        [Authorize]
        public IActionResult Index()
        {
			var currentuser = HttpContext.User.Identity.Name;
			ViewBag.currentuser = currentuser;
			//CookieOptions option = new CookieOptions();
			//optional
			//option.Expires = DateTime.Now.AddMinutes(20);
			//Response.Cookies.Append("UserName", currentuser, option);
			HttpContext.Session.SetString("UserName", currentuser);
            var hotel = _context.hotel.ToList();
            return View(hotel);
        }
		public IActionResult Rooms()
		{

		      var hotel=_context.hotel.ToList();
		      ViewBag.hotel = hotel;
			// ViewBag.currentuser = Request.Cookies["UserName"];
			 ViewBag.currentuser = HttpContext.Session.GetString("UserName");
		         var rooms=_context.rooms.ToList();  
			return View(rooms);
		}
		//the old code from teacher
		//public IActionResult RoomDetails()
		//{
		//	var hotel = _context.hotel.ToList();
		//	ViewBag.hotel = hotel;
		//	var roomDetail = _context.rooms.ToList();
		//	return View(roomDetail);
		//}

		//from me
		public IActionResult RoomDetails()
		{
			var hotel = _context.hotel.ToList();
			ViewBag.hotel = hotel;

			var roomDetails = _context.roomDetails.ToList();
			return View(roomDetails);
		}
		public IActionResult CreateNewHotel(Hotel hotels)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var hotel=_context.hotel.ToList();
            return View("Index", hotel);
        }
    }
}
