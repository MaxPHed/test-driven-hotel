using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotel.BLL;
using TestDrivenHotel.Domain;

namespace TestDrivenHotel.UI.Pages
{
    public class IndexModel : PageModel
    {
        public List<Room> Rooms { get; set; }
        private readonly RoomManager manager;
        [BindProperty]
        public DateTime? StartingDate { get; set; }

        [BindProperty]
        public DateTime EndingDate { get; set; }
        [BindProperty]
        public bool SingleRoom { get; set; }
        [BindProperty]
        public bool DoubleRoom { get; set; }

        public List<DateTime>? Dates
        {
            get { return HttpContext.Session.GetObject<List<DateTime>>("Dates"); }
            set { HttpContext.Session.SetObject("Dates", value); }
        }
        public IndexModel(RoomManager roomManager)
        {
            manager = roomManager;
        }
        public void OnGet()
        {
            if (Rooms == null)
            {
                if (Dates == null)
                {
                    Rooms = manager.returnAllRooms();
                }
                else
                {
                    Rooms = manager.ReturnAllAvailableRooms(Dates, "");
                }
            }

        }

        public IActionResult OnPost()
        {

            string action = Request.Form["action"];
            IActionResult myAction = null;
            switch (action)
            {
                case "CheckAvailability":
                    pressCheckAvailability();
                    break;
                case "BookRoom Single":
                    myAction = BookRoom("Single");
                    break;
                case "BookRoom Double":
                    myAction = BookRoom("Double");
                    break;
            }
            //HttpContext.Session.SetObject("Rooms", Rooms);
            //HttpContext.Session.SetObject("Dates", Dates);
            return myAction;
        }
        public void pressCheckAvailability()
        {
            Dates = DateManager.ReturnListOfDateTime(StartingDate, EndingDate);

            if (SingleRoom && !DoubleRoom)
            {
                Rooms = manager.ReturnAllAvailableRooms(Dates, "Single");
            }
            if (DoubleRoom && !SingleRoom)
            {
                Rooms = manager.ReturnAllAvailableRooms(Dates, "Double");
            }
            if (SingleRoom && DoubleRoom)
            {
                Rooms = manager.ReturnAllAvailableRooms(Dates, "");
            }
            //HttpContext.Session.SetObject("Rooms", Rooms);
        }

        public IActionResult BookRoom(string roomType)
        {
            //Rooms = manager.ReturnAllAvailableRooms(Dates, roomType);
            //Room roomToBook = Rooms.Where(r => r.Type == roomType).First();
            //manager.bookRoom(roomToBook.Id, Dates, roomToBook.Type);
            return RedirectToPage("/Book", new { roomType });
        }
    }
}
