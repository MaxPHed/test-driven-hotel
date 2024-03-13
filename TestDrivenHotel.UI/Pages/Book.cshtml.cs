using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.BLL;

namespace TestDrivenHotel.UI.Pages
{
    public class BookModel : PageModel
    {
        private readonly RoomManager manager;

        public string RoomType
        {
            get { return HttpContext.Session.GetObject<string>("RoomType"); }
            set { HttpContext.Session.SetObject("RoomType", value); }
        }
        public string Name
        {
            get { return HttpContext.Session.GetObject<string>("Name"); }
            set { HttpContext.Session.SetObject("Name", value); }
        }
        public List<DateTime>? Dates
        {
            get { return HttpContext.Session.GetObject<List<DateTime>>("Dates"); }
            set { HttpContext.Session.SetObject("Dates", value); }
        }
        public string BookingMessage { get; set; }
        public bool RoomBooked { get; set; }
        public BookModel(RoomManager roomManager)
        {
            manager = roomManager;
        }
        public void OnGet(string roomType)
        {
            Console.WriteLine("Book page loaded!");
            RoomType = roomType;
            RoomBooked = false;
        }

        public void OnPost()
        {
            Name = Request.Form["Name"];
            if (ValidateName(Name))
            {
                BookingMessage = manager.BookRoom(Dates, RoomType, Name);
                if (BookingMessage.StartsWith("Booking successfull")) { RoomBooked = true; }
            }
            else { BookingMessage = "Name is too short"; }
        }

        public bool ValidateName(string name)
        {
            if (name.Count() >= 2) { return true; } else { return false; }
        }
    }
}
