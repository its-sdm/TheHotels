using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheHotels.Data;
using TheHotels.Models;
using MimeKit;
using MailKit.Net.Smtp;


namespace TheHotels.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
		public async Task<string> SendEmail()
		{
			var Message = new MimeMessage();
			Message.From.Add(new MailboxAddress("Test Message", "sdeem1138@gmail.com"));
			Message.To.Add(MailboxAddress.Parse("younowsdm@gmail.com"));
			Message.Subject = "Test Email From My Project in Asp.net Core MVC";
			Message.Body=new TextPart("Plain")
			{
				Text = "Welcome to my App"
			};
			using (var client = new SmtpClient())
				try
				{
					client.Connect("smtp.gmail.com", 587);
					client.Authenticate("sdeem1138@gmail.com", "twsjbmmtwtntsjjt");
					await client.SendAsync(Message);
					client.Disconnect(true);
				}
				catch(Exception e) 
				{
					return e.Message.ToString();
				}
		
				return "Ok";
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
		public IActionResult DeleteRoom(int id)
		{
			var roomDelete = _context.rooms.SingleOrDefault(x => x.Id == id);
			if (roomDelete != null)
			{
				_context.rooms.Remove(roomDelete);
				_context.SaveChanges();
				TempData["Del"] = "Ok";
			}
			return RedirectToAction("Rooms");
		}
        public IActionResult DeleteRoomDetails(int id)
        {
            var rdDelete = _context.roomDetails.SingleOrDefault(x => x.Id == id);
            if (rdDelete != null)
            {
                _context.roomDetails.Remove(rdDelete);
                _context.SaveChanges();
                TempData["Del"] = "Ok";
            }
            return RedirectToAction("RoomDetails");
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

			var roomDetailsList = _context.roomDetails.ToList();
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
		//twsjbmmtwtntsjjt
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
		public IActionResult UpdateRoom(Rooms room)
		{
			if (ModelState.IsValid)
			{
				_context.rooms.Update(room);
				_context.SaveChanges();
				return RedirectToAction("Rooms");
			}
            return View("EditRoom");
        }

		public IActionResult EditRoom(int id)
		{
			var roomEdit = _context.rooms.SingleOrDefault(x => x.Id == id);
			return View(roomEdit);
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
        public IActionResult UpdateRoomDetails(RoomDetails roomDetail)
        {
            if (ModelState.IsValid)
            {
                _context.roomDetails.Update(roomDetail);
                _context.SaveChanges();
                return RedirectToAction("RoomDetails");
            }
            return View("EditRoomDetails");
        }

        public IActionResult EditRoomDetails(int id)
        {
            var roomDetailsEdit = _context.roomDetails.SingleOrDefault(x => x.Id == id);
            return View(roomDetailsEdit);
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
